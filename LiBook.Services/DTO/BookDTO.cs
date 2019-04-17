using System.Collections.Generic;

namespace LiBook.Services.DTO
{
    public class BookDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<AuthorBookDto> AuthorsBooks { get; set; }

    }
}