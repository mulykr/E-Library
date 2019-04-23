using LiBook.Data.Entities;
using System;

namespace LiBook.Services.DTO
{
    public class CommentDto
    {
        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public string Message { get; set; }

        public DateTime TimaStamp { get; set; }
    }
}
