using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Data;
using LiBook.Models;
using LiBook.Utilities.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LiBook.Models.DTO;
using LiBook.Services.Interfaces;
using AutoMapper;
using LiBook.Services;

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description")] BookViewModel book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookViewModel, Book > ()).CreateMapper();
                    
                    Book _book = new Book();
                    _book = mapper.Map<BookViewModel, Book>(book);
                    _service.Create(_book, file);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                    Book _book = new Book();
                    _book = mapper.Map<BookViewModel, Book>(book);
                    _service.Update(_book,file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
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
