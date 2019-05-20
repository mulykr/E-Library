using LiBook.Services.DTO;
using Microsoft.AspNetCore.Http;

namespace LiBook.Services.Interfaces
{
    public interface IBookService : IService<BookDto>
    {
        void AssignAuthor(string bookId, string authorId);

        void RemoveAuthors(string bookId);

        void AssignGenre(string bookId, string genreId);

        void RemoveGenres(string genreId);

        string UploadPdf(BookDto book, IFormFile file);
    }
}
