using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Models
{
    public class BookGenreViewModel
    {
        public string BookId { get; set; }

        public string GenreId { get; set; }

        public BookViewModel Book { get; set; }

        public GenreViewModel Genre { get; set; }
    }
}
