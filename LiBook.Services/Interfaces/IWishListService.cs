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
        void AddToWishList(ClaimsPrincipal principal, BookDto bookDto);
        void DeleteFromWishList(ClaimsPrincipal principal, BookDto bookDto);
        bool IsInWishList(ClaimsPrincipal principal, BookDto bookDto);
        int GetLikesCount(BookDto book);
    }
}
