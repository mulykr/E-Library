using LiBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Models
{
    public class AuthorLIkeViewModel
    {
        public string Id { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public bool Liked { get; set; }
    }
}
