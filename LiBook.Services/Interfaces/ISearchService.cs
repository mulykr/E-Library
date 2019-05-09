using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<BookDto> SearchBook(string key);
        IEnumerable<AuthorDto> SearchAuthor(string key);
    }
}
