﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Data.Repositories;
using LiBook.Services;
using LiBook.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LiBook.Tests.Servises
{
    public class BookServiceTests
    {
        [Fact]
        public void GetListTest()
        {
            // Arrange
            var list = GetTestCollectionDto();
            var svc = SetUpService();

            // Act
            IEnumerable<BookDto> actual = svc.GetList();

            // Assert
            Assert.Equal(actual.Count(), list.Count());
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void GetByIdTest(string id)
        {
            // Arrange
            var svc = SetUpService();
            var dtos = GetTestCollectionDto();
            var expected = dtos.First(i => i.Id == id);

            // Act
            var actual = svc.Get(id);

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ImagePath, actual.ImagePath);
        }

        [Fact]
        public void GetByIdNotExistingTest()
        {
            // Arrange
            var svc = SetUpService();

            // Act
            var actual = svc.Get("4");

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void CreateTest()
        {
            // Arrange
            var repository = new Mock<IRepository<Book>>();

            var mapper = new Mock<IMapper>();
            var svc = new BookService(repository.Object, mapper.Object);
            var mockFile = new Mock<IFormFile>();
            var expected = new BookDto
            {
                Id = "4",
                Title = "Fname",
                Description = "Lname",
            };

            // Act
            svc.Create(expected, mockFile.Object);

            // Assert
            mapper.Verify(m => m.Map<BookDto, Book>(It.IsAny<BookDto>()), Times.Once());
            repository.Verify(r => r.Create(It.IsAny<Book>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var expected = new BookDto
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Book
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object);
            var mockFile = new Mock<IFormFile>();
            

            // Act
            svc.Update(expected, mockFile.Object);

            // Assert
            mapper.Verify(m => m.Map<BookDto, Book>(It.IsAny<BookDto>()), Times.Once());
            repository.Verify(r => r.Update(It.IsAny<Book>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Book
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object);

            // Act
            svc.Delete(expected.Id);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        private IEnumerable<BookDto> GetTestCollectionDto()
        {
            return new[]
            {
                new BookDto
                {
                    Id = "1",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                },
                new BookDto
                {
                    Id = "2",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                },
                new BookDto
                {
                    Id = "3",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                }
            };
        }

        private IEnumerable<Book> GetTestCollection()
        {
            return new[]
            {
                new Book
                {
                    Id = "1",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                },
                new Book
                {
                    Id = "2",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                },
                new Book
                {
                    Id = "3",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg"
                }
            };
        }

        private BookService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(p => p.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Book>>().Setup(p => p.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Book>>().Setup(p => p.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(p => p.GetEnumerator()).Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.Books).Returns(mockSet.Object);

            var repository = new BookRepository(mockCtx.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDto>();
            });
            var mapper = mapperConfig.CreateMapper();
            var svc = new BookService(repository, mapper);
            return svc;
        }
    }
}
