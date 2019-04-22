using System;
namespace LiBook.Data.Entities
{
    public class WishListItem
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public UserProfile User { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Note { get; set; }
    }
}
