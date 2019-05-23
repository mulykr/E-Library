using LiBook.Services.DTO;
using System.Collections.Generic;

namespace LiBook.Services.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<BookDto> SearchBook(string key);
        IEnumerable<BookDto> SearchBookByGenre(string[] keys, string word = null);
        IEnumerable<AuthorDto> SearchAuthor(string key);
    }
}
