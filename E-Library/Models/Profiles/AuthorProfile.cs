using AutoMapper;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorViewModel, AuthorDto>();
        }
    }
}
