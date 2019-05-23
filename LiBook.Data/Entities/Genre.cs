using System.Collections.Generic;

namespace LiBook.Data.Entities
{
    public class Genre
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Color { get; set; }

        public ICollection<BookGenre> BooksGenres { get; set; }
    }
}
