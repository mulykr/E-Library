using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class AuthorBookProfile : Profile
    {
        public AuthorBookProfile()
        {
            CreateMap<AuthorBook, AuthorBookDto>();
            CreateMap<AuthorBookDto, AuthorBook>();
            CreateMap<AuthorBookDto, AuthorBookViewModel>();
            CreateMap<AuthorBookViewModel, AuthorBookDto>();

        }
    }
}
