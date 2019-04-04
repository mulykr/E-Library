using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Library.Data;
using E_Library.Models;
using LiBook.Data;
using LiBook.Utilities.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace LiBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepository<Book> _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BooksController(IRepository<Book> repository, 
            IHostingEnvironment env)
        {
            _repository = repository;
            _hostingEnvironment = env;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_repository.GetList());
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            var book = _repository.Get(id);
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
        public IActionResult Create([Bind("Id,Title,Description")] Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                    var resized = ImageTool.Resize(cropped, 500, 500);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "pics\\Books");
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    book.ImagePath = fileName;
                    resized.Save(filePath);
                }

                try
                {
                    _repository.Create(book);
                    _repository.Save();
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
            var book = _repository.Get(id);
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
        public IActionResult Edit(int id, [Bind("Id,Title,Description")] Book book, IFormFile file)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldImageName = _repository.Get(id).ImagePath;

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
                        book.ImagePath = fileName;
                    }
                    else
                    {
                        book.ImagePath = oldImageName;
                    }
                    _repository.Update(book);
                    _repository.Save();
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
            var book = _repository.Get(id);
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
            var book = _repository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            var imageName = book.ImagePath;
            _repository.Delete(id);
            _repository.Save();
            if (imageName != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath ?? "~\\wwwroot", "pics\\Books");
                var path = Path.Combine(uploads, imageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
