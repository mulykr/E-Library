using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LiBook.Services.Interfaces
{
    public interface IService<T> : IDisposable where T : class
    {
        IEnumerable<T> GetList();
        T Get(int id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
        
    }
}
