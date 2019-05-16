using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.DTO
{
    public class BookGenreDTO
    {
        public string BookId { get; set; }

        public string GenreId { get; set; }

        public BookDto Book { get; set; }

        public GenreDTO Genre { get; set; }
    }
}
