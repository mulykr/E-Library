using LiBook.Services.DTO;
using System.Collections.Generic;

namespace LiBook.Services.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<BookDto> SearchBook(string key);
        IEnumerable<AuthorDto> SearchAuthor(string key);
    }
}
