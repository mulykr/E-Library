using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.CodeAnalysis.Operations;

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
            try
            {
                var bookDto = _bookService.Get(id);
                var book = _mapper.Map<BookDto, BookViewModel>(bookDto);
                return View(book);
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

        public IActionResult AddCommentConfirmed(string id, string comment)
        {
            
            try
            {
                if (comment == null || comment.Length < 50 || comment.Length > 250)
                {
                    throw new ArgumentException("Comment text length must be between 50 and 250.\n", nameof(comment));
                }
                var commentViewModel = new CommentViewModel()
                {
                    BookId = id,
                    UserId = User.GetUserId(),
                    Message = comment
                };
                _service.AddComment(_mapper.Map<CommentViewModel, CommentDto>(commentViewModel));
                return Redirect($"/Books/Details/{id}");
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

        public IActionResult Delete(string id)
        {
            
            try
            {
                var wlDto = new CommentDto
                {
                    Id = id
                };
                var comment = _service.Get(id);
                _service.DeleteComment(wlDto);
                return Redirect($"/Books/Details/{comment.BookId}");
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
    }
}
