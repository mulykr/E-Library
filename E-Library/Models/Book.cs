using System.Collections.Generic;

namespace LiBook.Models
{
    public class Book
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double Price { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

    }
}