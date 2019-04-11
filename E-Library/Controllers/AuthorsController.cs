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

namespace LiBook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author> _repository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AuthorsController(IRepository<Author> repository,
            IHostingEnvironment env)
        {
            _repository = repository;
            _hostingEnvironment = env;
        }

       

        // GET: Authors
        public IActionResult Index()
        {
            return View( _repository.GetList());
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            var author = _repository.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Biography")] Author author, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var cropped = ImageTool.CropMaxSquare(Image.FromStream(file.OpenReadStream()));
                    var resized = ImageTool.Resize(cropped, 500, 500);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "pics\\Authors");
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    author.ImagePath = fileName;
                    resized.Save(filePath);
                }

                try
                {
                    _repository.Create(author);
                    _repository.Save();
                }
                catch (Exception e)
                {
                    return View(e.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int id)
        {
            var author = _repository.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Biography")] Author author, IFormFile file)
        {
            if (id != author.Id)
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

                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "pics\\Authors");
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
                        author.ImagePath = fileName;
                    }
                    else
                    {
                        author.ImagePath = oldImageName;
                    }
                    _repository.Update(author);
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int id)
        {
            var author = _repository.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var author = _repository.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            var imageName = author.ImagePath;
            _repository.Delete(id);
            _repository.Save();
            if (imageName != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath ?? "~\\wwwroot", "pics\\Authors");
                var path = Path.Combine(uploads, imageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
