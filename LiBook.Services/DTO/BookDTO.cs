using System.Collections.Generic;

namespace LiBook.Services.DTO
{
    public class BookDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string PdfFilePath { get; set; }

        public ICollection<BookGenreDTO> BooksGenres { get; set; }

        public ICollection<AuthorBookDto> AuthorsBooks { get; set; }

        public ICollection<WishListItemDto> WishListItems { get; set; }
    }
}