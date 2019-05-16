using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Data.Entities
{
    public class BookGenre
    {
        public string BookId { get; set; }

        public string GenreId { get; set; }

        public Book Book { get; set; }

        public Genre Genre { get; set; }
    }
}
