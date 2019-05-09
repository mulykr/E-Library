using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LiBook.Components
{
    public class SearchAuthor : ViewComponent
    {
        private readonly ISearchService _service;
        private readonly IMapper _mapper;

        public SearchAuthor(ISearchService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(string key)
        {
            var res = _service.SearchAuthor(key);
            List<AuthorViewModel> result = new List<AuthorViewModel>();
            foreach (var i in res)
            {
                result.Add(_mapper.Map<AuthorDto, AuthorViewModel>(i));
            }
            
            return View(result);
        }
    }
}
