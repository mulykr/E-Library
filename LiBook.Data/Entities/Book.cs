using System.Collections.Generic;

namespace LiBook.Data.Entities
{
    public class Book
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string PdfFilePath { get; set; }

        public Genre Genre { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

        public ICollection<WishListItem> WishListItems { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<BookGenre> BooksGenres { get; set; }
    }
}