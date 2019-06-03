using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorViewModel, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
        }
    }
}
