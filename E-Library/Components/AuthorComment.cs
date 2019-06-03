using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Components
{
    public class AuthorComment : ViewComponent
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public AuthorComment(ICommentService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(AuthorViewModel model)
        {
            var dto = _mapper.Map<AuthorViewModel, AuthorDto>(model);
            var comments = _service.GetByAuthor(dto)
                .Select(i => _mapper.Map<CommentDto, CommentViewModel>(i));
            return View(comments);
        }
    }
}
