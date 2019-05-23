using LiBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiBook.Models
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        [MinLength(50)]
        [MaxLength(250)]
        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public ICollection<CommentLikeViewModel> CommentLikes { get; set; }
    }
}
