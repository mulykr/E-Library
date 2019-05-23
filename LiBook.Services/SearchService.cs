using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LiBook.Services
{
    public class SearchService:ISearchService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _repositoryAuthor;
        private readonly IRepository<Book> _repositoryBook;

        public SearchService(IRepository<Author> repositoryAuthor,
            IRepository<Book> repositoryBook,
            IMapper mapper)
        {
            _repositoryAuthor = repositoryAuthor;
            _repositoryBook = repositoryBook;
            _mapper = mapper;
        }

        public SearchService(IRepository<Author> repositoryAuthor,
            IMapper mapper)
        {
            _repositoryAuthor = repositoryAuthor;
            _mapper = mapper;
        }

        public SearchService(IRepository<Book> repositoryBook,
            IMapper mapper)
        {
            _repositoryBook = repositoryBook;
            _mapper = mapper;
        }

        public IEnumerable<AuthorDto> SearchAuthor(string key)
        {
            var found = _repositoryAuthor
                .Get(i => i.FirstName.ToLower().Contains(key.ToLower()) || i.LastName.ToLower().Contains(key.ToLower()))
                .Select(i => _mapper.Map<Author, AuthorDto>(i));

            return found;
        }

        public IEnumerable<BookDto> SearchBook(string key)
        {
            var found = _repositoryBook.Get(i => i.Title.ToLower().Contains(key.ToLower()))
                .Select(i => _mapper.Map<Book, BookDto>(i));

            return found;
        }

        public IEnumerable<BookDto> SearchBookByGenre(string[] keys, string word = null)
        {
            var res = _repositoryBook.GetList();
            var found = res.Where(j => j.BooksGenres.Any(t => keys.Contains(t.GenreId)))
                .Select(m => _mapper.Map<Book, BookDto>(m));
            if (!string.IsNullOrEmpty(word))
            {
                found = found.Where(i => i.Title.ToLower().Contains(word.ToLower()));
            }
            return found;
        }

    }
}
