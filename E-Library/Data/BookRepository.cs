using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data;
using LiBook.Models;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data
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
            return _context.Books;
        }

        public Book Get(int id)
        {
            return _context.Books
                .AsNoTracking()
                .Include(item => item.AuthorsBooks)
                .ThenInclude(item => item.Author)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter = null, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Book> query = _context.Books;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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
        }

        public void Update(Book item)
        {
            _context.Books.Update(item);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
                _context.Books.Remove(book);
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
