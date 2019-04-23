using System.Linq;
using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiBook.Components
{
    public class BookComments : ViewComponent
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public BookComments(ICommentService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(BookViewModel model)
        {
            var dto = _mapper.Map<BookViewModel, BookDto>(model);
            var comments = _service.GetByBook(dto)
                .Select(i => _mapper.Map<CommentDto, CommentViewModel>(i));
            return View(comments);
        }
    }
}
