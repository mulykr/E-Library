using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LiBook.Components
{
    public class SearchBook:ViewComponent
    {
        private readonly ISearchService _service;
        private readonly IMapper _mapper;

        public SearchBook(ISearchService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(string key)
        {
            var res = _service.SearchBook(key);
            List<BookViewModel> result = new List<BookViewModel>();
            foreach(var i in res)
            {
                result.Add(_mapper.Map< BookDto, BookViewModel>(i));
            }
            
            return View(result);
        }
    }
}
