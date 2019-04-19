using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Models
{
    public class AuthorViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public string Biography { get; set; }

        public ICollection<AuthorBookViewModel> AuthorsBooks { get; set; }
    }
}
