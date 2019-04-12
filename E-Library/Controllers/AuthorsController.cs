using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Models;
using LiBook.Services.DTO;
using Microsoft.AspNetCore.Http;
using LiBook.Services.Interfaces;
using System.Linq;

namespace LiBook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Authors
        public IActionResult Index()
        {
            return View(_service.GetList().Select(item => _mapper.Map<AuthorDto, AuthorViewModel>(item)));
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            var author = _service.Get(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AuthorDto,AuthorViewModel>(author));
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
                    var newAuthor = _mapper.Map<AuthorViewModel, AuthorDto>(author);
                    _service.Create(newAuthor, file);
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
            return View(_mapper.Map<AuthorDto, AuthorViewModel>(author));
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
                    var updateAuthor = _mapper.Map<AuthorViewModel, AuthorDto>(author);
                    _service.Update(updateAuthor, file);
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

            return View(_mapper.Map<AuthorDto, AuthorViewModel>(author));
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
