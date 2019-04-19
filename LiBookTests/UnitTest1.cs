using System;
using Xunit;
using Moq;
using LiBook.Services.Interfaces;
using System.Collections.Generic;
using LiBook.Services.DTO;
using LiBook.Controllers;
using AutoMapper;
using LiBook.Models.Profiles;

namespace LiBookTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
           // IMapper _mapper = new BookProfile();

            var dataSource = new Mock<IBookService>();
            dataSource.Setup(a => a.GetList()).Returns(new List<BookDto>());
          //  BooksController book = new BooksController(dataSource.Object, _mapper);
        }
    }
}
