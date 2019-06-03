using LiBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.DTO
{
    public class AuthorLikeDTO
    {
        public string Id { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string AuthorId { get; set; }
        public AuthorDto Author { get; set; }
        public bool Liked { get; set; }
    }
}
