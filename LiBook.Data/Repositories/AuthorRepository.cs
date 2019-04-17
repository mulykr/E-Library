using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IEnumerable<Author> GetList()
        {
            return _context.Authors;
        }

        public Author Get(string id)
        {
            return _context.Authors
                .AsNoTracking()
                .Include(item => item.AuthorsBooks)
                .ThenInclude(item => item.Book)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Author> Get(Expression<Func<Author, bool>> filter = null, Func<IQueryable<Author>, IOrderedQueryable<Author>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Author> query = _context.Authors;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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

        public void Create(Author item)
        {
            _context.Authors.Add(item);
        }

        public void Update(Author item)
        {
            _context.Authors.Update(item);
        }

        public void Delete(string id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
                _context.Authors.Remove(author);
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
