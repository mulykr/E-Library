using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IEnumerable<Book> GetList()
        {
            return _context.Books
                .Include(i => i.BooksGenres)
                .ThenInclude(i => i.Genre)
                .Include(i => i.AuthorsBooks)
                .ThenInclude(i => i.Author);
        }

        public Book Get(string id)
        {
            return _context.Books
                .AsNoTracking()
                .Include(i => i.BooksGenres)
                .ThenInclude(i => i.Genre)
                .Include(item => item.AuthorsBooks)
                .ThenInclude(item => item.Author)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter = null, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Book> query = _context.Books
                .Include(i => i.BooksGenres)
                .ThenInclude(i => i.Genre)
                .Include(i => i.AuthorsBooks)
                .ThenInclude(i => i.Author);
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

        public void Create(Book item)
        {
            _context.Books.Add(item);
            _context.SaveChanges();

        }

        public void Update(Book item)
        {
            _context.Books.Update(item);
            _context.SaveChanges();

        }

        public void Delete(string id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
                _context.Books.Remove(book);
            _context.SaveChanges();

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
