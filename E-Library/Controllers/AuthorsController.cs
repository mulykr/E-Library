using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiBook.Models;
using LiBook.Services.DTO;
using Microsoft.AspNetCore.Http;
using LiBook.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
            try
            {
                return View(_service.GetList().Select(item => _mapper.Map<AuthorDto, AuthorViewModel>(item)));
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

        // GET: Authors/Details/5
        public IActionResult Details(string id)
        {
            
            try
            {
                var author = _service.Get(id);
                if (author == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<AuthorDto, AuthorViewModel>(author));
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
        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = Request.HttpContext.TraceIdentifier,
                        Exception = e
                    });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        [Authorize(Roles = "Admin")]
        // GET: Authors/Edit/5
        public IActionResult Edit(string id)
        {          
            try
            {
                var author = _service.Get(id);
                if (author == null)
                {
                    return NotFound();
                }

            return View(_mapper.Map<AuthorDto, AuthorViewModel>(author));
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
        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,FirstName,LastName,Biography")] AuthorViewModel author, IFormFile file)
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
            return View(author);
        }

        [Authorize(Roles = "Admin")]
        // GET: Authors/Delete/5
        public IActionResult Delete(string id)
        {
            try
            {
                var author = _service.Get(id);
                if (author == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<AuthorDto, AuthorViewModel>(author));
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

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(string id)
        {
            try
            {
                _service.Delete(id);

                return RedirectToAction(nameof(Index));
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

        public IActionResult Like(string id)
        {
            try
            {
                _service.Like(id, User);
                var authorId = _service.Get(id).Id;
                return RedirectToAction("Details", "Authors", new { id = authorId });
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

        private bool AuthorExists(string id)
        {
            return _service.Get(id) != null;
        }
    }
}
