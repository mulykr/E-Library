using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddComment(string id)
        {
            var bookDto = _bookService.Get(id);
            var book = _mapper.Map<BookDto, BookViewModel>(bookDto);
            return View(book);
        }

        public IActionResult AddCommentConfirmed(string id, string message)
        {
            var bookDto = _bookService.Get(id);
            var wlDto = new CommentDto
            {
                BookId = bookDto.Id,
                UserId = User.GetUserId(),
                Message = message
            };
            _service.AddComment(wlDto);
            return Redirect("/");
        }

        public IActionResult RemoveComment(string id)
        {
            var bookDto = _bookService.Get(id);
            var wlDto = new CommentDto
            {
                BookId = bookDto.Id,
                UserId = User.GetUserId()
            };
            _service.DeleteComment(wlDto);
            return Redirect("/");
        }
    }
}
