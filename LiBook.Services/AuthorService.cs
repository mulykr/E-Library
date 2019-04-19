using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using AutoMapper;
using LiBook.Data.Entities;
using Microsoft.AspNetCore.Http;
using LiBook.Utilities.Images;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;

namespace LiBook.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _repository;
        private readonly IAppConfiguration _configuration;

        public AuthorService(IRepository<Author> repository,
            IMapper mapper,
            IAppConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IEnumerable<AuthorDto> GetList()
        {
            return _repository.GetList().Select(item => _mapper.Map<Author, AuthorDto>(item));
        }

        public AuthorDto Get(string id)
        {
            return _mapper.Map<Author, AuthorDto>(_repository.Get(id));
        }

        public void Create(AuthorDto item, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);
                var uploads = Path.Combine(_configuration.WebRootPath, "pics\\Authors");
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);
                item.ImagePath = fileName;
                resized.Save(filePath);
            }

            var author = _mapper.Map<AuthorDto, Author>(item);
            _repository.Create(author);
            _repository.Save();

        }

        public void Update(AuthorDto item, IFormFile file)
        {
            var oldImageName = Get(item.Id).ImagePath;

            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);

                var uploads = Path.Combine(_configuration.WebRootPath, "pics\\Authors");
                if (!string.IsNullOrEmpty(oldImageName))
                {
                    var oldPath = Path.Combine(uploads, oldImageName);
                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
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

            var author = _mapper.Map<AuthorDto, Author>(item);
            _repository.Update(author);
            _repository.Save();

        }

        public void Delete(string id)
        {
            var author = Get(id);
            var imageName = author.ImagePath;
            if (imageName != null)
            {
                var uploads = Path.Combine(_configuration.WebRootPath, "pics\\Authors");
                var path = Path.Combine(uploads, imageName);
                if (File.Exists(path))
                {
                    File.Delete(path);
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
