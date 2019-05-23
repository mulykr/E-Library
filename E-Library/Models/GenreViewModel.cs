using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Models
{
    public class GenreViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<BookGenreViewModel> BookGenre { get; set; }
    }
}
