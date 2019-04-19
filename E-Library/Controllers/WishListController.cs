using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiBook.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly IWishListService _service;

        public WishListController(IWishListService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var items = _service.GetUserWishList(User);
            return View(items);
        }
    }
}