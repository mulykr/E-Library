using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Models.DTO
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public string Biography { get; set; }

        public ICollection<AuthorBookViewModel> AuthorsBooks { get; set; }
    }
}
