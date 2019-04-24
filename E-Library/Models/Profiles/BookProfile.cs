using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto, BookViewModel>();
            CreateMap<BookViewModel, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}
