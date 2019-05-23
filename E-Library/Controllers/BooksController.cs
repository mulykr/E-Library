using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Models;
using Microsoft.AspNetCore.Http;
using LiBook.Services.Interfaces;
using AutoMapper;
using LiBook.Services.DTO;
using Microsoft.AspNetCore.Authorization;

namespace LiBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BooksController(IBookService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Books
        public IActionResult Index()
        {
            try
            {
                return View(_service.GetList().Select(item => _mapper.Map<BookDto, BookViewModel>(item)));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        public IActionResult Search(string []genres, string word = null)
        {
            try
            {
                return View("Search", new object[]{genres, word});
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        // GET: Books/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var book = _service.Get(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<BookDto, BookViewModel>(book));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,Title,Genre,Description")] BookViewModel book, IFormFile file, IFormFile pdf)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newBook = _mapper.Map<BookViewModel, BookDto>(book);
                    newBook.PdfFilePath = _service.UploadPdf(newBook, pdf);
                    _service.Create(newBook, file);
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = Request.HttpContext.TraceIdentifier,
                        Exception = e
                    });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            try
            {
                var book = _service.Get(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<BookDto, BookViewModel>(book));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id, [Bind("Id,Title,Genre,Description")] BookViewModel book, IFormFile file, IFormFile pdf)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBook = _mapper.Map<BookViewModel, BookDto>(book);
                    if (pdf!=null)
                    {
                        updatedBook.PdfFilePath = _service.UploadPdf(_mapper.Map<BookViewModel, BookDto>(book), pdf);
                    }
                    _service.Update(updatedBook, file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = Request.HttpContext.TraceIdentifier,
                        Exception = e
                    });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AssignAuthors(string id)
        {
            try
            {
                var book = _service.Get(id);
                var bookViewModel = _mapper.Map<BookDto, BookViewModel>(book);
                return View(bookViewModel);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AssignAuthors(string id, string[] authors)
        {
            try
            {
                _service.RemoveAuthors(id);
                foreach (var authorId in authors)
                {
                    _service.AssignAuthor(id, authorId);
                }

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AssignGenres(string id)
        {
            try
            {
                var book = _service.Get(id);
                var bookViewModel = _mapper.Map<BookDto, BookViewModel>(book);
                return View(bookViewModel);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AssignGenres(string id, string[] genres)
        {
            try
            {
                _service.RemoveGenres(id);
                foreach (var genreId in genres)
                {
                    _service.AssignGenre(id, genreId);
                }

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
            {
                var book = _service.Get(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<BookDto, BookViewModel>(book));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }
        
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(string id)
        {
            //try
            //{
                _service.Delete(id);

                return RedirectToAction(nameof(Index));
            //}
            //catch (Exception e)
            //{
            //    return View("Error", new ErrorViewModel
            //    {
            //        RequestId = Request.HttpContext.TraceIdentifier,
            //        Exception = e
            //    });
            //}
        }

        private bool BookExists(string id)
        {
            return _service.Get(id) != null;
        }
    }
}
