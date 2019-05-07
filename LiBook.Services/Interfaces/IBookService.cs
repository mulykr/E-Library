using LiBook.Services.DTO;

namespace LiBook.Services.Interfaces
{
    public interface IBookService : IService<BookDto>
    {
        void AssignAuthor(string bookId, string authorId);

        void RemoveAuthors(string bookId);
    }
}
