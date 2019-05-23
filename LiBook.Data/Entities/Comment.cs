using System;
using System.Collections.Generic;

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

        public DateTime TimeStamp { get; set; }

        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}
