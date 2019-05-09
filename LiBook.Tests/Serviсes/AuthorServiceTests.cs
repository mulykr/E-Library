using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Data.Repositories;
using LiBook.Services;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LiBook.Tests.Serviсes
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public AuthorServiceTests()
        {
            _config = new Mock<IAppConfiguration>();
            _config.Setup(c => c.WebRootPath).Returns(string.Empty);
        }

        [Fact]
        public void GetListTest()
        {
            // Arrange
            var list = GetTestCollectionDto();
            var svc = SetUpService();

            // Act
            IEnumerable<AuthorDto> actual = svc.GetList();

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
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
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
            var repository = new Mock<IRepository<Author>>();
            var mapper = new Mock<IMapper>();
            var svc = new AuthorService(repository.Object, mapper.Object, _config.Object);
            var mockFile = new Mock<IFormFile>();
            var expected = new AuthorDto
            {
                Id = "4",
                FirstName = "Fname",
                LastName = "Lname",
                Biography = "Some biography"
            };
            
            // Act
            svc.Create(expected, mockFile.Object);

            // Assert
            mapper.Verify(m => m.Map<AuthorDto, Author>(It.IsAny<AuthorDto>()), Times.Once());
            repository.Verify(r => r.Create(It.IsAny<Author>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var expected = new AuthorDto
            {
                Id = "1",
                FirstName = "Fname",
                LastName = "Lname",
                Biography = "Some biography"
            };
            var repository = new Mock<IRepository<Author>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Author
            {
                Id = "1",
                FirstName = "Fname",
                LastName = "Lname",
                Biography = "Some biography"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Author, AuthorDto>(It.IsAny<Author>())).Returns(expected);
            var svc = new AuthorService(repository.Object, mapper.Object, _config.Object);
            var mockFile = new Mock<IFormFile>();
            

            // Act
            svc.Update(expected, mockFile.Object);

            // Assert
            mapper.Verify(m => m.Map<AuthorDto, Author>(It.IsAny<AuthorDto>()), Times.Once());
            repository.Verify(r => r.Update(It.IsAny<Author>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var expected = new AuthorDto
            {
                Id = "1",
                FirstName = "Fname",
                LastName = "Lname",
                Biography = "Some biography"
            };
            var repository = new Mock<IRepository<Author>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Author
            {
                Id = "1",
                FirstName = "Fname",
                LastName = "Lname",
                Biography = "Some biography"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Author, AuthorDto>(It.IsAny<Author>())).Returns(expected);
            var svc = new AuthorService(repository.Object, mapper.Object, _config.Object);
            
            // Act
            svc.Delete(expected.Id);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DisposeTest()
        {
            // Arrange
            var svc = SetUpService();

            // Act
            svc.Dispose();

            // Assert
        }

        private IEnumerable<AuthorDto> GetTestCollectionDto()
        {
            return new[]
            {
                new AuthorDto
                {
                    Id = "1",
                    FirstName = "Stephen",
                    LastName = "King",
                    Biography = "Some biography",
                    ImagePath = "Authors/1.jpg"
                },
                new AuthorDto
                {
                    Id = "2",
                    FirstName = "Joanne",
                    LastName = "Rowling",
                    Biography = "Some biography",
                    ImagePath = "Authors/2.jpg"
                },
                new AuthorDto
                {
                    Id = "3",
                    FirstName = "Taras",
                    LastName = "Shevchenko",
                    Biography = "Some biography",
                    ImagePath = "Authors/3.jpg"
                }
            };
        }

        private IEnumerable<Author> GetTestCollection()
        {
            return new[]
            {
                new Author
                {
                    Id = "1",
                    FirstName = "Stephen",
                    LastName = "King",
                    Biography = "Some biography",
                    ImagePath = "Authors/1.jpg"
                },
                new Author
                {
                    Id = "2",
                    FirstName = "Joanne",
                    LastName = "Rowling",
                    Biography = "Some biography",
                    ImagePath = "Authors/2.jpg"
                },
                new Author
                {
                    Id = "3",
                    FirstName = "Taras",
                    LastName = "Shevchenko",
                    Biography = "Some biography",
                    ImagePath = "Authors/3.jpg"
                }
            };
        }

        private AuthorService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<Author>>();
            mockSet.As<IQueryable<Author>>().Setup(p => p.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Author>>().Setup(p => p.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Author>>().Setup(p => p.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Author>>().Setup(p => p.GetEnumerator()).Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.Authors).Returns(mockSet.Object);

            var repository = new AuthorRepository(mockCtx.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
            });
            var mapper = mapperConfig.CreateMapper();
            var svc = new AuthorService(repository, mapper, _config.Object);
            return svc;
        }
    }
}
