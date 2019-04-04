using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Models
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