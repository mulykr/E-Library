using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Data.Entities
{
    public class AuthorLike
    {
        public string Id { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public bool Liked { get; set; }
    }
}
