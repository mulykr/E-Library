using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
using System.Security.Claims;

namespace LiBook.Tests.Serviсes
{
    public class CommentServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public CommentServiceTests()
        {
            _config = new Mock<IAppConfiguration>();
            _config.Setup(c => c.WebRootPath).Returns(string.Empty);
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
            Assert.Equal(expected.Message, actual.Message);
        }
        [Fact]
        public void GetByBookTest()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Id = "1",
                Title = "Stephen",
                Description = "King"
            };
            var list = GetTestCollection();
            var expected = list.First(i => i.BookId == bookDto.Id);
            var repository = new Mock<IRepository<Comment>>();
            repository.Setup(r => r.Get(i => i.BookId == expected.Id, null, "")).Returns(list.Where(l => l.BookId == expected.Id));
            var mapper = new Mock<IMapper>();
            var svc = new CommentService(repository.Object, mapper.Object);

            // Act
            svc.GetByBook(bookDto);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<Expression<Func<Comment, bool>>>(), null, ""), Times.Once());
        }

        [Fact]
        public void GetByUserTest()
        {
            // Arrange
            var user = new UserProfile
            {
                Id = "1"
            };

            var list = GetTestCollection();
            var expected = list.First(i => i.UserId == user.Id);
            var repository = new Mock<IRepository<Comment>>();
            repository.Setup(r => r.Get(i => i.UserId == expected.Id, null, "")).Returns(list.Where(l => l.UserId == expected.Id));
            var principal = new Mock<ClaimsPrincipal>();
            principal.Setup(b => b.Identity.IsAuthenticated).Returns(true);
            principal.Setup(b => b.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

            var mapper = new Mock<IMapper>();
            var svc = new CommentService(repository.Object, mapper.Object);

            // Act
            svc.GetByUser(principal.Object);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<Expression<Func<Comment, bool>>>(), null, ""), Times.Once());
        }

        //Roman please check that test!
        [Fact]
        public void AddCommentTest()
        {
            // Arrange
            var expected = new CommentDto
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
                Message = "Best book ever",
                TimeStamp = DateTime.Now
            };
            var repository = new Mock<IRepository<Comment>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Comment
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
                    Message = "Best book ever",
                    TimeStamp = DateTime.Now
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Comment, CommentDto>(It.IsAny<Comment>())).Returns(expected);
            mapper.Setup(m => m.Map<CommentDto, Comment>(It.IsAny<CommentDto>())).Returns(new Comment());
            var svc = new CommentService(repository.Object, mapper.Object);

            // Act
            svc.AddComment(expected);

            // Assert
            mapper.Verify(m => m.Map<CommentDto, Comment>(It.IsAny<CommentDto>()), Times.Once());
            repository.Verify(r => r.Create(It.IsAny<Comment>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());

        }
        [Fact]
        public void DeleteCommentTest()
        {
            // Arrange
            var expected = new CommentDto
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
                Message = "Best book ever",
                TimeStamp = DateTime.Now
            };

            var repository = new Mock<IRepository<Comment>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new Comment
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
                Message = "Best book ever",
                TimeStamp = DateTime.Now
            });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Comment, CommentDto>(It.IsAny<Comment>())).Returns(expected);
            var svc = new CommentService(repository.Object, mapper.Object);

            // Act
            svc.DeleteComment(expected);

            // Assert
            repository.Verify(r => r.Get(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }


        private IEnumerable<Comment> GetTestCollection()
        {
            return new[]
            {
                new Comment
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
                    Message = "Best book ever"
                },
                new Comment
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
                    Message = "Best book ever I read"
                },
                new Comment
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
                    Message = "Best book ever me read"
                }
            };
        }
        private IEnumerable<CommentDto> GetTestCollectionDto()
        {
            return new[]
            {
                new CommentDto
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
                    Message = "Best book ever"
                },
                new CommentDto
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
                    Message = "Best book ever I read"
                },
                new CommentDto
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
                    Message = "Best book ever me read"
                }
            };
        }
        private CommentService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>()
                .Setup(p => p.Provider)
                .Returns(list.Provider);
            mockSet.As<IQueryable<Comment>>()
                .Setup(p => p.Expression)
                .Returns(list.Expression);
            mockSet.As<IQueryable<Comment>>()
                .Setup(p => p.ElementType)
                .Returns(list.ElementType);
            mockSet.As<IQueryable<Comment>>()
                .Setup(p => p.GetEnumerator())
                .Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.Comments)
                .Returns(mockSet.Object);

            var repository = new CommentRepository(mockCtx.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentDto>();
            });

            var mapper = mapperConfig.CreateMapper();

            var svc = new CommentService(repository, mapper);
            return svc;
        }
    }
}
