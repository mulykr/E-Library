using System.Collections.Generic;

namespace LiBook.Models.DTO
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<AuthorBookViewModel> AuthorsBooks { get; set; }

    }
}