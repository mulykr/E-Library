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
using Microsoft.AspNetCore.Http;
using LiBook.Utilities.Images;
using System.IO;
using System.Drawing;

namespace LiBook.Services
{
    public class BookService:IBookService
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

        public void Create(Book item, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "pics\\Books");
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);
                item.ImagePath = fileName;
                resized.Save(filePath);
            }

            _repository.Create(item);
            _repository.Save();
        }

        public void Update(Book item, IFormFile file)
        {

            var oldImageName = Get(item.Id).ImagePath;

            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);

                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "pics\\Books");
                if (!string.IsNullOrEmpty(oldImageName))
                {
                    var oldPath = Path.Combine(uploads, oldImageName);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);
                resized.Save(filePath);
                item.ImagePath = fileName;
            }
            else
            {
                item.ImagePath = oldImageName;
            }
            _repository.Update(item);
            _repository.Save();

        }

        public void Delete(int id)
        {
            var book = Get(id);
           
            var imageName = book.ImagePath;
            if (imageName != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath ?? "~\\wwwroot", "pics\\Books");
                var path = Path.Combine(uploads, imageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _repository.Delete(id);
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
