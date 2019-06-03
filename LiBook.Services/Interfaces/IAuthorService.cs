using LiBook.Services.DTO;
using System.Security.Claims;

namespace LiBook.Services.Interfaces
{
    public interface IAuthorService:IService<AuthorDto>
    {
        void Like(string authorId, ClaimsPrincipal user);
    }
}
