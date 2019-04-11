using System;
using System.Collections.Generic;
using System.Text;
using LiBook.Services.Interfaces;
using LiBook.Models.DTO;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data;
using LiBook.Models;
using Microsoft.AspNetCore.Hosting;

namespace LiBook.Services.Services
{
    public class BookService:IService<Book>
    {

        private readonly IRepository<Book> _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookService(IRepository<Book> repository,
            IHostingEnvironment env)
        {
            _repository = repository;
            _hostingEnvironment = env;
        }

        public IEnumerable<Book> GetList()
        {
            return _repository.GetList();
        }

        public Book Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter = null, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null, string includeProperties = "")
        {
            return _repository.Get(filter,orderBy,includeProperties);       
        }

        public void Create(Book item)
        {
            _repository.Create(item);
        }

        public void Update(Book item)
        {
            _repository.Update(item);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Save()
        {
            _repository.Save();
        }

        #region Dispose pattern

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repository.Dispose();
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
