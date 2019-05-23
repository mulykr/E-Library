using System;
using System.Collections.Generic;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using LiBook.Utilities.Images;
using System.IO;
using System.Drawing;
using System.Linq;
using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;

namespace LiBook.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Book> _repository;
        private readonly IAppConfiguration _appConfiguration;

        public BookService(IRepository<Book> repository,
            IMapper mapper,
            IAppConfiguration appConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
        }

        public IEnumerable<BookDto> GetList()
        {
            return _repository.GetList().Select(item => _mapper.Map<Book, BookDto>(item));
        }

        public BookDto Get(string id)
        {
            return _mapper.Map<Book, BookDto>(_repository.Get(id));
        }

        public void Create(BookDto item, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);
                var uploads = Path.Combine(_appConfiguration.WebRootPath, "pics\\Books");
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);
                item.ImagePath = fileName;
                resized.Save(filePath);
            }

            var book = _mapper.Map<BookDto, Book>(item);
            _repository.Create(book);
            _repository.Save();
        }

        public void AssignAuthor(string bookId, string authorId)
        {
            var book = _repository.Get(i => i.Id == bookId).First();
            if (book.AuthorsBooks.Any(i => i.AuthorId == authorId && i.BookId == bookId)) return;
            book.AuthorsBooks.Add(new AuthorBook
            {
                BookId = bookId,
                AuthorId = authorId
            });

            _repository.Update(book);
            _repository.Save();
        }

        public void RemoveAuthors(string bookId)
        {
            var book = _repository.Get(i => i.Id == bookId).First();
            if (!book.AuthorsBooks.Any()) return;

            book.AuthorsBooks.Clear();

            _repository.Update(book);
            _repository.Save();
        }

        public void AssignGenre(string bookId, string genreId)
        {
            var book = _repository.Get(i => i.Id == bookId).First();
            if (book.BooksGenres.Any(i => i.GenreId == genreId && i.BookId == bookId)) return;
            book.BooksGenres.Add(new BookGenre
            {
                BookId = bookId,
                GenreId= genreId
            });

            _repository.Update(book);
            _repository.Save();
        }

        public void RemoveGenres(string genreId)
        {
            var book = _repository.Get(i => i.Id == genreId).First();
            if (!book.BooksGenres.Any()) return;

            book.BooksGenres.Clear();

            _repository.Update(book);
            _repository.Save();
        }

        public string UploadPdf(BookDto bookDto, IFormFile file)
        {
            if (file != null)
            {
                var uploadFolder = Path.Combine(_appConfiguration.WebRootPath, "pdf");
                var fileExists = bookDto.PdfFilePath != null && 
                                 File.Exists(Path.Combine(uploadFolder, bookDto.PdfFilePath));
                var extension = Path.GetExtension(file.FileName);
                if (!_appConfiguration.AllowedFileExtensions.Contains(extension.ToLower()))
                {
                    throw new ArgumentException("Wrong file extension");
                }

                var newFileName = Guid.NewGuid() + extension;
                var stream = new FileStream(Path.Combine(uploadFolder, newFileName), FileMode.OpenOrCreate);
                file.CopyTo(stream);
                stream.Dispose();
                if (fileExists)
                {
                    File.Delete(Path.Combine(uploadFolder, bookDto.PdfFilePath));
                }
                
                return newFileName;
            }

            return null;
        }

        public void Update(BookDto item, IFormFile file)
        {

            var oldImageName = Get(item.Id).ImagePath;

            if (file != null && file.Length > 0)
            {
                var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                var resized = ImageTool.Resize(cropped, 500, 500);

                var uploads = Path.Combine(_appConfiguration.WebRootPath, "pics\\Books");
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

            var book = _mapper.Map<BookDto, Book>(item);
            _repository.Update(book);
            _repository.Save();

        }

        public void Delete(string id)
        {
            var book = Get(id);
           
            var imageName = book.ImagePath;
            if (imageName != null)
            {
                var uploads = Path.Combine(_appConfiguration.WebRootPath, "pics\\Books");
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
