using LiBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.DTO
{
    public class CommentDTO
    {
        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public string Message { get; set; }

        public DateTime TimaStamp { get; set; }
    }
}
