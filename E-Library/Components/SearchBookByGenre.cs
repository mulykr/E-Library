using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using AutoMapper;

namespace LiBook.Components
{
    public class SearchBookByGenre: ViewComponent
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;


        public SearchBookByGenre(IBookService service,
             IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        public IViewComponentResult Invoke(string []keys)
        {
            var res = _service.GetList();
            List<BookViewModel> result = new List<BookViewModel>();
            var q = new List<BookDto>();
                for (int i = 0; i < keys.Length;i++)
                {
                     q= res.Where(j => j.BooksGenres.Any(t => t.GenreId == keys[i]) == true).ToList();
                }
            

            foreach (var w in q)
            {
                result.Add(_mapper.Map<BookDto, BookViewModel>(w));
            }
            
            return View(result);
        }
    }
}
