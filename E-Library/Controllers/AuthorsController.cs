using System;
using System.Drawing;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Utilities.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LiBook.Services.Interfaces;

namespace LiBook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }



        // GET: Authors
        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            var author = _service.Get(id);
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
        public IActionResult Create([Bind("Id,FirstName,LastName,Biography")] AuthorViewModel author, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorViewModel, AuthorDto>()).CreateMapper();
                    var item = mapper.Map<AuthorViewModel, AuthorDto>(author);
                    _service.Create(item, file);
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
            var author = _service.Get(id);
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
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Biography")] AuthorViewModel author, IFormFile file)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorViewModel, AuthorDto>()).CreateMapper();
                    var item = mapper.Map<AuthorViewModel, AuthorDto>(author);
                    _service.Update(item, file);
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
            var author = _service.Get(id);
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
            _service.Delete(id);           
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _service.Get(id) != null;
        }
    }
}
