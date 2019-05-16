using LiBook.Services.DTO;
using Microsoft.AspNetCore.Http;

namespace LiBook.Services.Interfaces
{
    public interface IBookService : IService<BookDto>
    {
        void AssignAuthor(string bookId, string authorId);

        void RemoveAuthors(string bookId);

        void AssignGanre(string bookId, string ganreId);

        void RemoveGanre(string ganreId);

        string UploadPdf(BookDto book, IFormFile file);
    }
}
