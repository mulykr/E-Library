using System;
using System.Collections.Generic;
using System.Security.Claims;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Services.Interfaces
{
    public interface IWishListService : IDisposable
    {
        WishListItem Get(string id);
        IEnumerable<WishListItem> GetUserWishList(ClaimsPrincipal principal);
        void AddToWishList(ClaimsPrincipal principal, BookDto bookDto);
        void DeleteFromWishList(ClaimsPrincipal principal, BookDto bookDto);
        bool IsInWishList(ClaimsPrincipal principal, BookDto bookDto);
    }
}
