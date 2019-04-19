using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiBook.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetList();
        T Get(string id);
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Create(T item);
        void Update(T item);
        void Delete(string id);
        void Save();
        
    }
}
