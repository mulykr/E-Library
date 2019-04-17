using System.Collections.Generic;

namespace LiBook.Services.DTO
{
    public class AuthorDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public string Biography { get; set; }

        public ICollection<AuthorBookDto> AuthorsBooks { get; set; }
    }
}
