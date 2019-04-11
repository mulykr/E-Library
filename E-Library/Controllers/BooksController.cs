using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Models;
using Microsoft.AspNetCore.Http;
using LiBook.Models.DTO;
using LiBook.Services.Interfaces;
using AutoMapper;

namespace LiBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            var book = _service.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
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
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book > ()).CreateMapper();
                    var newBook = mapper.Map<BookViewModel, Book>(book);
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
            return View(book);
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
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book>()).CreateMapper();

                    var updatedBook = mapper.Map<BookViewModel, Book>(book);
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

            return View(book);
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
