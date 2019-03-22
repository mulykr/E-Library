using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }

        public string BookId { get; set; }

        public string AuthorId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}