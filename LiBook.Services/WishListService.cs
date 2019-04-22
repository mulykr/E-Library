using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;

namespace LiBook.Services
{
    public class WishListService : IWishListService
    {
        private readonly IRepository<WishListItem> _repository;
        private readonly IMapper _mapper;

        public WishListService(IRepository<WishListItem> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }


        public WishListItemDto Get(string id)
        {
            return _mapper.Map<WishListItem, WishListItemDto>(_repository.Get(id));
        }

        public IEnumerable<WishListItemDto> GetUserWishList(ClaimsPrincipal principal)
        {
            return _repository
                .Get(i => i.UserId == principal.GetUserId())
                .Select(i => _mapper.Map<WishListItem, WishListItemDto>(i));
        }

        public void AddToWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            _repository.Create(new WishListItem
            {
                BookId = bookDto.Id,
                UserId = principal.GetUserId(),
                TimeStamp = DateTime.Now
            });

            _repository.Save();
        }

        public void DeleteFromWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            var item = _repository.Get(i =>
                i.BookId == bookDto.Id && i.UserId == principal.GetUserId())
                .First();
            if (item != null)
            {
                _repository.Delete(item.Id);
                _repository.Save();
            }
        }

        public bool IsInWishList(ClaimsPrincipal principal, BookDto bookDto)
        {
            return _repository.Get(i =>
                       i.BookId == bookDto.Id && i.UserId == principal.GetUserId())
                       .Any();
        }

        public int GetLikesCount(BookDto book)
        {
            return _repository.Get(i => i.BookId == book.Id).Count();
        }
    }
}
