using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Models.DTO
{
    public class AuthorBookViewModel
    {
        public int Id { get; set; }

        public string BookId { get; set; }

        public string AuthorId { get; set; }

        public BookViewModel Book { get; set; }

        public AuthorViewModel Author { get; set; }
    }
}
