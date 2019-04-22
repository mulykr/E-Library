using System.Linq;
using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiBook.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly IWishListService _service;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public WishListController(IWishListService service, IBookService bookService, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            var items = _service.GetUserWishList(User).Select(i => _mapper.Map<WishListItemDto, WishListItemViewModel>(i));
            return View(items);
        }

        [Authorize]
        public IActionResult AddToWishList(string id)
        {
            var bookDto = _bookService.Get(id);
            _service.AddToWishList(User, bookDto);
            return Redirect("/WishList");
        }

        [Authorize]
        public IActionResult RemoveFromWishList(string id)
        {
            var bookDto = _bookService.Get(id);
            _service.DeleteFromWishList(User, bookDto);
            return Redirect("/WishList");
        }
    }
}