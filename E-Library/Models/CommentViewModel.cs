using LiBook.Data.Entities;
using System;

namespace LiBook.Models
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
