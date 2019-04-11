using Microsoft.AspNetCore.Http;
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

        
        void Create(T item, IFormFile file);
        void Update(T item, IFormFile file);
        void Delete(int id);
        
    }
}
