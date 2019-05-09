using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Data.Repositories;
using LiBook.Services;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace LiBook.Tests.Serviсes
{
    public class WishListServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public WishListServiceTests()
        {
            _config = new Mock<IAppConfiguration>();
            _config.Setup(c => c.WebRootPath)
                .Returns(string.Empty);
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
            Assert.Equal(expected.BookId, actual.BookId);
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.Note, actual.Note);
        }

        [Fact]
        public void GetUserWishListTest()
        {
            // Arrange
            var user = new UserProfile
            {
                Id = "1"
            };

            var list = GetTestCollection();
            var expected = list.First(i => i.UserId == user.Id);
            var repository = new Mock<IRepository<WishListItem>>();
            repository.Setup(r => r.Get(i => i.UserId == expected.Id, null, "")).Returns(list.Where(l => l.UserId == expected.Id));
            var principal = new Mock<ClaimsPrincipal>();
            principal.Setup(b => b.Identity.IsAuthenticated).Returns(true);
            principal.Setup(b => b.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

            var mapper = new Mock<IMapper>();
            var svc = new WishListService(repository.Object, mapper.Object);

            // Act
            svc.GetUserWishList(principal.Object);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<Expression<Func<WishListItem, bool>>>(), null, ""), Times.Once());
        }

        [Fact]
        public void AddToWishListTest()
        {
            // Arrange
            var expected = new WishListItemDto
            {
                Id = "1",
                BookId = "1",
                Book = new Book()
                {
                    Id = "1",
                    Title = "Stephen",
                    Description = "King"
                },
                UserId = "1",
                User = new UserProfile()
                {
                    Id = "1",
                    FirstName = "Oleksii",
                    LastName = "Rudenko"
                },
                Note = "Best book ever"
            };

            var repository = new Mock<IRepository<WishListItem>>();
            repository.Setup(r => r.Get(expected.Id))
                .Returns(new WishListItem
                {
                    Id = "1",
                    BookId = "1",
                    Book = new Book()
                    {
                        Id = "1",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "1",
                    User = new UserProfile()
                    {
                        Id = "1",
                        FirstName = "Oleksii",
                        LastName = "Rudenko"
                    },
                    Note = "Best book ever"
                });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<WishListItemDto, WishListItem>(It.IsAny<WishListItemDto>())).Returns(new WishListItem());
            var svc = new WishListService(repository.Object, mapper.Object);

            // Act
            svc.AddToWishList(expected);

            // Assert
            mapper.Verify(m => m.Map<WishListItemDto, WishListItem>(It.IsAny<WishListItemDto>()), Times.Once());
            repository.Verify(r => r.Create(It.IsAny<WishListItem>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DeleteFromWishListTest()
        {
            // Arrange
            var item = GetTestCollectionDto().First(i => i.BookId == "1" && i.UserId == "1");
            var expected = GetTestCollection().First(i => i.BookId == item.BookId && i.UserId == item.UserId);
            var repository = new Mock<IRepository<WishListItem>>();

            repository.Setup(r => r.Get(It.IsAny<Expression<Func<WishListItem, bool>>>(), null, ""))
                .Returns(new List<WishListItem> {expected});

            var mapper = new Mock<IMapper>();
            var svc = new WishListService(repository.Object, mapper.Object);

            // Act
            svc.DeleteFromWishList(item);


            // Assert
            repository.Verify(r => r.Get(It.IsAny<Expression<Func<WishListItem, bool>>>(), null, string.Empty), Times.Once());
            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        private IEnumerable<WishListItem> GetTestCollection()
        {
            return new[]
            {
                new WishListItem
                {
                    Id = "1",
                    BookId = "1",
                    Book = new Book()
                    {
                        Id = "1",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "1",
                    User = new UserProfile()
                    {
                        Id = "1",
                        FirstName = "Oleksii",
                        LastName = "Rudenko"
                    },
                    Note = "Best book ever"
                },
                new WishListItem
                {
                    Id = "2",
                    BookId = "2",
                    Book = new Book()
                    {
                        Id = "2",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "2",
                    User = new UserProfile()
                    {
                        Id = "2",
                        FirstName = "Maxym",
                        LastName = "Hrybun"
                    },
                    Note = "Best book ever I read"
                },
                new WishListItem
                {
                    Id = "3",
                    BookId = "3",
                    Book = new Book()
                    {
                        Id = "3",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "3",
                    User = new UserProfile()
                    {
                        Id = "3",
                        FirstName = "Alex",
                        LastName = "Medyk"
                    },
                    Note = "Best book ever me read"
                }
            };
        }

        private IEnumerable<WishListItemDto> GetTestCollectionDto()
        {
            return new[]
            {
                new WishListItemDto
                {
                    Id = "1",
                    BookId = "1",
                    Book = new Book()
                    {
                        Id = "1",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "1",
                    User = new UserProfile()
                    {
                        Id = "1",
                        FirstName = "Oleksii",
                        LastName = "Rudenko"
                    },
                    Note = "Best book ever"
                },
                new WishListItemDto
                {
                    Id = "2",
                    BookId = "2",
                    Book = new Book()
                    {
                        Id = "2",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "2",
                    User = new UserProfile()
                    {
                        Id = "2",
                        FirstName = "Maxym",
                        LastName = "Hrybun"
                    },
                    Note = "Best book ever I read"
                },
                new WishListItemDto
                {
                    Id = "3",
                    BookId = "3",
                    Book = new Book()
                    {
                        Id = "3",
                        Title = "Stephen",
                        Description = "King"
                    },
                    UserId = "3",
                    User = new UserProfile()
                    {
                        Id = "3",
                        FirstName = "Alex",
                        LastName = "Medyk"
                    },
                    Note = "Best book ever me read"
                }
            };
        }
        private WishListService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<WishListItem>>();
            mockSet.As<IQueryable<WishListItem>>()
                .Setup(p => p.Provider)
                .Returns(list.Provider);
            mockSet.As<IQueryable<WishListItem>>()
                .Setup(p => p.Expression)
                .Returns(list.Expression);
            mockSet.As<IQueryable<WishListItem>>()
                .Setup(p => p.ElementType)
                .Returns(list.ElementType);
            mockSet.As<IQueryable<WishListItem>>()
                .Setup(p => p.GetEnumerator())
                .Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.WishListItems)
                .Returns(mockSet.Object);

            var repository = new WishItemsRepository(mockCtx.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WishListItem, WishListItemDto>();
            });

            var mapper = mapperConfig.CreateMapper();

            var svc = new WishListService(repository,mapper);
            return svc;
        }
    }
}
