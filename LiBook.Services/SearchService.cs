using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<AuthorDto> SearchAuthor(string key)
        {
            var allAuthors = _repositoryAuthor.GetList();
            List<AuthorDto> listOfResults = new List<AuthorDto>();
            foreach (var item in allAuthors)
            {
                if (item.FirstName.Contains(key) || item.LastName.Contains(key))
                {
                    listOfResults.Add(_mapper.Map<Author, AuthorDto>(item));
                }
            }
            return listOfResults;
        }

        public IEnumerable<BookDto> SearchBook(string key)
        {
            var allBooks=_repositoryBook.GetList();
            List<BookDto> listOfResults = new List<BookDto>();
            foreach(var item in allBooks)
            {
                if(item.Title.Contains(key))
                {
                    listOfResults.Add(_mapper.Map<Book, BookDto>(item));
                }
            }
            return listOfResults;
        }

        
    }
}
