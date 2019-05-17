using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiBook.Data.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public void Create(Genre item)
        {
            _context.Genres.Add(item);
        }

        public void Delete(string id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
                _context.Genres.Remove(genre);
        }

        public Genre Get(string id)
        {
            return _context.Genres
                .AsNoTracking()
                .Include(item => item.BooksGenres)
                .ThenInclude(item => item.Genre)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Genre> Get(Expression<Func<Genre, bool>> filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Genre> query = _context.Genres
                .AsNoTracking()
                .Include(i => i.BooksGenres)
                .ThenInclude(i => i.Genre);
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

        public IEnumerable<Genre> GetList()
        {
            return _context.Genres
                .AsNoTracking()
                .Include(i => i.BooksGenres)
                .ThenInclude(i => i.Book);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Genre item)
        {
            _context.Genres.Update(item);
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
