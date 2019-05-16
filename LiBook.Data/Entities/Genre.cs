using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Data.Entities
{
    public class Genre
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<BookGenre> BooksGenres { get; set; }
    }
}
