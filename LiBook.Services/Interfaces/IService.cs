using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace LiBook.Services.Interfaces
{
    public interface IService<T> : IDisposable where T : class
    {
        IEnumerable<T> GetList();
        T Get(string id);
        void Create(T item, IFormFile file);
        void Update(T item, IFormFile file);
        void Delete(string id);
    }
}
