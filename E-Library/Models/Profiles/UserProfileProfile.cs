using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Models.Account;

namespace LiBook.Models.Profiles
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfile, UserProfileViewModel>();
            CreateMap<UserProfileViewModel, UserProfile>();
        }
    }
}
