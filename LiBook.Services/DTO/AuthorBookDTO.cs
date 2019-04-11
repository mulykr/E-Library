namespace LiBook.Services.DTO
{
    public class AuthorBookDto
    {
        public int Id { get; set; }

        public string BookId { get; set; }

        public string AuthorId { get; set; }

        public BookDto Book { get; set; }

        public AuthorDto Author { get; set; }
    }
}
