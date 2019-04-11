using System.Collections.Generic;

namespace LiBook.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

    }
}