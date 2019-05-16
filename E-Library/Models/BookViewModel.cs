using System.Collections.Generic;

namespace LiBook.Models
{
    public class BookViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string PdfFilePath { get; set; }

        public ICollection<BookGenreViewModel> BooksGenres { get; set; } 

        public ICollection<AuthorBookViewModel> AuthorsBooks { get; set; }
    }
}
