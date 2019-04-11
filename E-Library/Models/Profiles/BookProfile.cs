using AutoMapper;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto, BookViewModel>();
            CreateMap<BookViewModel, BookDto>();
        }
    }
}
