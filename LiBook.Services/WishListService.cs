using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Services.DTO;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Services
{
    public class WishListService : IWishListService
    {
        private readonly ApplicationDbContext _context;

        public WishListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public WishListItem Get(string id)
        {
            return _context.WishListItems.Find(id);
        }

        public IEnumerable<WishListItem> GetUserWishList(ClaimsPrincipal principal)
        {
            return _context.WishListItems
                .Include(i => i.Book)
                .Where(i => i.UserId == principal.GetUserId());
        }

        public void AddToWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            _context.WishListItems.Add(new WishListItem
            {
                BookId = bookDto.Id,
                UserId = principal.GetUserId(),
                TimeStamp = DateTime.Now
            });

            _context.SaveChanges();
        }

        public void DeleteFromWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            var item = _context.WishListItems.FirstOrDefault(i =>
                i.BookId == bookDto.Id && i.UserId == principal.GetUserId());
            if (item != null)
            {
                _context.WishListItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public bool IsInWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            return _context.WishListItems.FirstOrDefault(i =>
                       i.BookId == bookDto.Id && i.UserId == principal.GetUserId()) != null;
        }
    }
}
