using System;
using System.Collections.Generic;
using System.Security.Claims;
using LiBook.Services.DTO;

namespace LiBook.Services.Interfaces
{
    public interface IWishListService : IDisposable
    {
        WishListItemDto Get(string id);
        IEnumerable<WishListItemDto> GetUserWishList(ClaimsPrincipal principal);
        void AddToWishList(WishListItemDto wishListItemDto);
        void DeleteFromWishList(WishListItemDto wishListItemDto);
        bool IsInWishList(ClaimsPrincipal principal, BookDto bookDto);
        int GetLikesCount(BookDto book);
    }
}
