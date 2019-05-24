using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class BookServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public BookServiceTests()
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
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);
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
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);
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
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

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

        [Fact]
        public void AssignAuthorTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new [] {
                    new Book
                {
                    Id = "1",
                    Title = "Fname",
                    Description = "Lname",
                    AuthorsBooks = new List<AuthorBook>()
                }
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);
            
            // Act
            svc.AssignAuthor("1", "1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
            repository.Verify(i => i.Save(), Times.Once());
        }

        [Fact]
        public void AssignGenreTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                {
                    Id = "1",
                    Title = "Fname",
                    Description = "Lname",
                    BooksGenres = new List<BookGenre>()
                }
            });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.AssignGenre("1", "1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
            repository.Verify(i => i.Save(), Times.Once());
        }

        [Fact]
        public void AssignExistingAuthorTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        AuthorsBooks = new List<AuthorBook>()
                        {
                            new AuthorBook()
                            {
                                BookId = "1",
                                AuthorId = "1"
                            }
                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.AssignAuthor("1", "1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Never());
            repository.Verify(i => i.Save(), Times.Never());
        }

        [Fact]
        public void AssignExistingGenreTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        AuthorsBooks = new List<AuthorBook>()
                        {
                            new AuthorBook()
                            {
                                BookId = "1",
                                AuthorId = "1"
                            }
                        },
                        BooksGenres = new List<BookGenre>()
                        {
                            new BookGenre()
                            {
                                BookId = "1",
                                GenreId = "1",
                                Genre = new Genre
                                {
                                    Id = "1",
                                    Name = "Genre"
                                }
                            }
                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.AssignAuthor("1", "1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Never());
            repository.Verify(i => i.Save(), Times.Never());
        }

        [Fact]
        public void RemoveAuthorsTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        AuthorsBooks = new List<AuthorBook>()
                        {
                            new AuthorBook()
                            {
                                BookId = "1",
                                AuthorId = "1"
                            }
                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.RemoveAuthors("1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
            repository.Verify(i => i.Save(), Times.Once());
        }

        [Fact]
        public void RemoveGenreTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        BooksGenres = new List<BookGenre>()
                        {
                            new BookGenre()
                            {
                                BookId = "1",
                                GenreId = "1"
                            }
                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.RemoveGenres("1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
            repository.Verify(i => i.Save(), Times.Once());
        }

        [Fact]
        public void RemoveNoneAuthorsTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        AuthorsBooks = new List<AuthorBook>()
                        {
                            
                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.RemoveAuthors("1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Never());
            repository.Verify(i => i.Save(), Times.Never());
        }

        [Fact]
        public void RemoveNoneGenresTest()
        {
            // Arrange
            var expected = new BookDto()
            {
                Id = "1",
                Title = "Fname",
                Description = "Lname"
            };
            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.Get(It.IsAny<Expression<Func<Book, bool>>>(), null, ""))
                .Returns(new[] {
                    new Book
                    {
                        Id = "1",
                        Title = "Fname",
                        Description = "Lname",
                        AuthorsBooks = new List<AuthorBook>()
                        {
                            
                        },
                        BooksGenres = new List<BookGenre>()
                        {

                        }
                    }
                });
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Book, BookDto>(It.IsAny<Book>())).Returns(expected);
            var svc = new BookService(repository.Object, mapper.Object, _config.Object);

            // Act
            svc.RemoveGenres("1");

            // Assert
            repository.Verify(i => i.Update(It.IsAny<Book>()), Times.Never());
            repository.Verify(i => i.Save(), Times.Never());
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
            var svc = new BookService(repository, mapper, _config.Object);
            return svc;
        }
    }
}
