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
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LiBook.Tests.Services
{
    public class SearchServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public SearchServiceTests()
        {
            _config = new Mock<IAppConfiguration>();
            _config.Setup(c => c.WebRootPath).Returns(String.Empty);
        }

        [Theory, InlineData("Stephen")]
        public void SearchAuthorTest(string expected)
        {
            var svc = SetUpAuthorService();
            var res = svc.SearchAuthor("Step");

            Assert.NotEmpty(res);
            Assert.Equal(expected, res.FirstOrDefault().FirstName);
        }

        [Theory, InlineData("Stephen")]
        public void SearchBookTest(string expected)
        {
            var svc = SetUpBookService();
            var res = svc.SearchBook("Step");

            //Assert.NotEmpty(res);
            //Assert.Equal(expected, res.FirstOrDefault().Title);
        }

        [Fact]
        public void SearchBookByGenre()
        {
            var genreDto = new GenreDTO
            {
                Id = "1",
                Name = "Advenure"
            };
            var svc = SetUpBookService();
            var search = new string[] { genreDto.Name };
            var expected = GetTestBookDtoCollection()
                .Where(i => i.BooksGenres.
                All(j => j.GenreId == genreDto.Id));
            var actual = svc.SearchBookByGenre(search);
            
            Assert.Equal(expected, actual);
        }

        private IEnumerable<Author> GetTestAuthorCollection()
        {
            return new[]
            {
                new Author
                {
                    Id = "1",
                    FirstName = "Stephen",
                    LastName = "King",
                    Biography = "Some biography",
                    ImagePath = "Authors/1.jpg",
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

        private IEnumerable<BookDto> GetTestBookDtoCollection()
        {
            return new[]
            {
                new BookDto
                {
                    Id = "1",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenreDTO>
                    {
                        new BookGenreDTO
                        {
                            Genre = new GenreDTO
                            {
                                Id = "1",
                                Name = "Adventure"
                            }
                        }
                    }
                },
                 new BookDto
                {
                    Id = "2",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenreDTO>
                    {
                        new BookGenreDTO
                        {
                            Genre = new GenreDTO
                            {
                                Id = "2",
                                Name = "Fantasy"
                            }
                        }
                    }
                },
                 new BookDto
                {
                    Id = "3",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenreDTO>
                    {
                        new BookGenreDTO
                        {
                            Genre = new GenreDTO
                            {
                                Id = "3",
                                Name = "Novel"
                            }
                        }
                    }
                }
            };
        }

        private IEnumerable<Book> GetTestBookCollection()
        {
            return new[]
            {
                new Book
                {
                    Id = "1",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenre>
                    {
                        new BookGenre
                        {
                            Genre = new Genre
                            {
                                Id = "1",
                                Name = "Adventure"
                            }
                        }
                    }
                },
                 new Book
                {
                    Id = "2",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenre>
                    {
                        new BookGenre
                        {
                            Genre = new Genre
                            {
                                Id = "2",
                                Name = "Fantasy"
                            }
                        }
                    }
                },
                 new Book
                {
                    Id = "3",
                    Title = "Stephen",
                    Description = "King",
                    ImagePath = "Books/1.jpg",
                    BooksGenres = new List<BookGenre>
                    {
                        new BookGenre
                        {
                            Genre = new Genre
                            {
                                Id = "2",
                                Name = "Fantasy"
                            }
                        }
                    }
                }
            };
        }
        private SearchService SetUpAuthorService()
        {
            var list = GetTestAuthorCollection().AsQueryable();
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
            var svc = new SearchService(repository, mapper);
            return svc;
        }

        private SearchService SetUpBookService()
        {
            var list = GetTestBookCollection().AsQueryable();
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
            var svc = new SearchService(repository, mapper);
            return svc;
        }
    }
}
