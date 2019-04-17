namespace LiBook.Models
{
    public class AuthorBookViewModel
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public string AuthorId { get; set; }

        public BookViewModel Book { get; set; }

        public AuthorViewModel Author { get; set; }
    }
}
