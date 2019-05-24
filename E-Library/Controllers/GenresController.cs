using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Services.Interfaces;
using AutoMapper;
using LiBook.Services.DTO;
using LiBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace LiBook.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GenresController(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Genres
        public IActionResult Index()
        {
            try
            {
                return View(_service.GetList().Select(item => _mapper.Map<GenreDTO, GenreViewModel>(item)));
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
        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Color")] GenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGenre = _mapper.Map<GenreViewModel, GenreDTO>(genre);
                    _service.Create(newGenre);
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
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Genres/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {
                var genre = _service.Get(id);
                if (genre == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<GenreDTO, GenreViewModel>(genre));
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
        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,Name,Color")] GenreViewModel genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updateGenre = _mapper.Map<GenreViewModel, GenreDTO>(genre);
                    _service.Update(updateGenre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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
            return View(genre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Genres/Delete/5
        public IActionResult Delete(string id)
        {
            try
            {
                var genre = _service.Get(id);
                if (genre == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<GenreDTO, GenreViewModel>(genre));
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

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

        private bool GenreExists(string id)
        {
            return _service.Get(id) != null;
        }
    }
}
