using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Models;
using Microsoft.AspNetCore.Http;
using LiBook.Services.Interfaces;
using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BooksController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_service.GetList().Select(item => _mapper.Map<BookDto, BookViewModel>(item)));
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            var book = _service.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<BookDto, BookViewModel>(book));
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description")] BookViewModel book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newBook = _mapper.Map<BookViewModel, BookDto>(book);
                    _service.Create(newBook, file);
                }
                catch (Exception e)
                {
                    return View(e.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _service.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<BookDto, BookViewModel>(book));
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description")] BookViewModel book, IFormFile file)
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
                    _service.Update(updatedBook,file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int id)
        {
            var book = _service.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<BookDto, BookViewModel>(book));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _service.Get(id) != null;
        }
    }
}
