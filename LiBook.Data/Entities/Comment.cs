using System;

namespace LiBook.Data.Entities
{
    public class Comment
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public string Message { get; set; }

        public DateTime TimaStamp { get; set; }
    }
}
