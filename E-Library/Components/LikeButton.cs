using AutoMapper;
using LiBook.Models;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LiBook.Components
{
    public class LikeButton : ViewComponent
    {
        private readonly IWishListService _wishListService;
        private readonly IMapper _mapper;
        public LikeButton(IWishListService wishListService, IMapper mapper)
        {
            _wishListService = wishListService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(BookViewModel model)
        {
            var dto = _mapper.Map<BookViewModel, BookDto>(model);
            var count = _wishListService.GetLikesCount(dto);
            var liked = _wishListService.IsInWishList(UserClaimsPrincipal, dto);
            if (liked)
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<a href=\"/WishList/RemoveFromWishList/{model.Id}\" class=\"btn btn-danger fa fa-heart\">\t{count}</a>"));
            }

            return new HtmlContentViewComponentResult(
                new HtmlString($"<a href=\"/WishList/AddToWishList/{model.Id}\" class=\"btn btn-danger fa fa-heart-o\">\t{count}</a>"));
        }
    }
}
