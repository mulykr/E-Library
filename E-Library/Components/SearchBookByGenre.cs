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
        private readonly ISearchService _service;
        private readonly IMapper _mapper;


        public SearchBookByGenre(ISearchService service,
             IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        public IViewComponentResult Invoke(string []genres, string word)
        {
            List<BookViewModel> result = new List<BookViewModel>();
            var res = _service.SearchBookByGenre(genres, word);
            foreach (var w in res)
            {
                result.Add(_mapper.Map<BookDto, BookViewModel>(w));
            }
            return View(result);
        }
    }
}
