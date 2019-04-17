using System.Collections.Generic;

namespace LiBook.Data.Entities
{
    public class Author
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public string Biography { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}