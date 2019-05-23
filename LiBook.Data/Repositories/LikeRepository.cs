using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data.Repositories
{
    public class LikeRepository : IRepository<CommentLike>
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IEnumerable<CommentLike> GetList()
        {
            return _context.CommentLikes
                .Include(i => i.UserProfile);
        }

        public CommentLike Get(string id)
        {
            return _context.CommentLikes
                .AsNoTracking()
                .Include(item => item.UserProfile)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<CommentLike> Get(Expression<Func<CommentLike, bool>> filter = null, Func<IQueryable<CommentLike>, IOrderedQueryable<CommentLike>> orderBy = null, string includeProperties = "")
        {
            IQueryable<CommentLike> query = _context.CommentLikes
                .Include(i => i.UserProfile);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Create(CommentLike item)
        {
            _context.CommentLikes.Add(item);

        }

        public void Update(CommentLike item)
        {
            _context.CommentLikes.Update(item);

        }

        public void Delete(string id)
        {
            var like = _context.CommentLikes.Find(id);
            if (like != null)
                _context.CommentLikes.Remove(like);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
