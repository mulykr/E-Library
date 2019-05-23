using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.DTO
{
    public class GenreDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<BookGenreDTO> BookGenre { get; set; }
    }
}
