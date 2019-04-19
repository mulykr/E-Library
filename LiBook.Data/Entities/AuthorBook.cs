namespace LiBook.Data.Entities
{
    public class AuthorBook
    {
        public string BookId { get; set; }

        public string AuthorId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}