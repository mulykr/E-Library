using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class WishListProfile : Profile
    {
        public WishListProfile()
        {
            CreateMap<WishListItemViewModel, WishListItemDto>();
            CreateMap<WishListItemDto, WishListItemViewModel>();
            CreateMap<WishListItemDto, WishListItem>();
            CreateMap<WishListItem, WishListItemDto>();
        }
    }
}
