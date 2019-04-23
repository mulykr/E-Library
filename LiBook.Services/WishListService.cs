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
                .OrderBy(i => i.TimeStamp)
                .Reverse()
                .Select(i => _mapper.Map<WishListItem, WishListItemDto>(i));
        }

        public void AddToWishList(WishListItemDto wishListItemDto)
        {
            var exists = _repository.Get(i => i.BookId == wishListItemDto.BookId && i.UserId == wishListItemDto.UserId).Any();
            if (exists)
            {
                return;
            }

            var item = _mapper.Map<WishListItemDto, WishListItem>(wishListItemDto);
            item.TimeStamp = DateTime.Now;
            _repository.Create(item);
            _repository.Save();
        }

        public void DeleteFromWishList(WishListItemDto wishListItemDto)
        {
            var item = _repository.Get(i =>
                i.BookId == wishListItemDto.BookId && i.UserId == wishListItemDto.UserId)
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
