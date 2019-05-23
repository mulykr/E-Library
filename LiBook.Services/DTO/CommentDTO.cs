using LiBook.Data.Entities;
using System;
using System.Collections.Generic;

namespace LiBook.Services.DTO
{
    public class CommentDto
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }
        public ICollection<CommentLikeDto> CommentLikes { get; set; } 
    }
}
