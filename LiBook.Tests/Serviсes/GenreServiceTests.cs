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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LiBook.Tests.Serviсes
{
    public class GenreServiceTests
    {
        [Fact]
        public void GetListTest()
        {
            // Arrange
            var list = GetTestCollectionDto();
            var svc = SetUpService();

            // Act
            IEnumerable<GenreDTO> actual = svc.GetList();

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
            Assert.Equal(expected.Name, actual.Name);
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
        public void UpdateTest()
        {
            // Arrange
            var expected = new GenreDTO
            {
                Id = "1",
                Name = "Adventure"
            };
            var repository = new Mock<IRepository<Genre>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Genre
            {
                Id = "1",
                Name = "Adventure"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Genre, GenreDTO>(It.IsAny<Genre>())).Returns(expected);
            var svc = new GenreService(repository.Object, mapper.Object);


            // Act
            svc.Update(expected);

            // Assert
            mapper.Verify(m => m.Map<GenreDTO, Genre>(It.IsAny<GenreDTO>()), Times.Once());
            repository.Verify(r => r.Update(It.IsAny<Genre>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void CreateTest()
        {
            // Arrange
            var repository = new Mock<IRepository<Genre>>();

            var mapper = new Mock<IMapper>();
            var svc = new GenreService(repository.Object, mapper.Object);
            //var mockFile = new Mock<IFormFile>();
            var expected = new GenreDTO
            {
                Id = "4",
                Name = "Fname"
            };

            // Act
            svc.Create(expected);

            // Assert
            mapper.Verify(m => m.Map<GenreDTO, Genre>(It.IsAny<GenreDTO>()), Times.Once());
            repository.Verify(r => r.Create(It.IsAny<Genre>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var expected = new GenreDTO()
            {
                Id = "1",
                Name = "Fantasy"
            };
            var repository = new Mock<IRepository<Genre>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Genre
            {
                Id = "1",
                Name = "Fantasy"
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Genre, GenreDTO>(It.IsAny<Genre>())).Returns(expected);
            var svc = new GenreService(repository.Object, mapper.Object);

            // Act
            svc.Delete(expected.Id);

            // Assert

            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        private IEnumerable<GenreDTO> GetTestCollectionDto()
        {
            return new[]
            {
                new GenreDTO
                {
                    Id = "1",
                    Name = "Horror"
                },
                new GenreDTO
                {
                    Id = "2",
                    Name = "Adventure"
                },
                new GenreDTO
                {
                    Id = "3",
                    Name = "Fantasy"
                }
            };
        }

        private IEnumerable<Genre> GetTestCollection()
        {
            return new[]
            {
                new Genre
                {
                   Id = "1",
                    Name = "Horror"
                },
                new Genre
                {
                   Id = "2",
                    Name = "Adventure"
                },
                new Genre
                {
                     Id = "3",
                    Name = "Fantasy"
                }
            };
        }

        private GenreService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<Genre>>();
            mockSet.As<IQueryable<Genre>>().Setup(p => p.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Genre>>().Setup(p => p.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Genre>>().Setup(p => p.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Genre>>().Setup(p => p.GetEnumerator()).Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.Genres).Returns(mockSet.Object);

            var repository = new GenreRepository(mockCtx.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreDTO>();
            });

            var mapper = mapperConfig.CreateMapper();

            var svc = new GenreService(repository, mapper);
            return svc;
        }
    }
}
