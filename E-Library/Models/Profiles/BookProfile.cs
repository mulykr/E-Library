using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
