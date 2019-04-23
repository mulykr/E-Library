using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiBook.Controllers
{
    [Authorize]
    public class CommentController:Controller
    {
        private readonly ICommentService _service;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService service, IBookService bookService, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _bookService = bookService;
        }

        public IActionResult AddComment(string id)
        {
            var bookDto = _bookService.Get(id);
            var book = _mapper.Map<BookDto, BookViewModel>(bookDto);
            return View(book);
        }

        public IActionResult AddCommentConfirmed(string id, string comment)
        {
            var wlDto = new CommentDto
            {
                BookId = id,
                UserId = User.GetUserId(),
                Message = comment
            };
            _service.AddComment(wlDto);
            return Redirect($"/Books/Details/{id}");
        }

        public IActionResult Delete(string id)
        {
            var wlDto = new CommentDto
            {
                Id = id
            };
            var comment = _service.Get(id);
            _service.DeleteComment(wlDto);
            return Redirect($"/Books/Details/{comment.BookId}");
        }
    }
}
