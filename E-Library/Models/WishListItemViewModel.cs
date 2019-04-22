using System;
using LiBook.Data.Entities;

namespace LiBook.Models
{
    public class WishListItemViewModel
    {
        public string BookId { get; set; }
        public BookViewModel Book { get; set; }
        public string UserId { get; set; }
        public UserProfile User { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Note { get; set; }
    }
}
