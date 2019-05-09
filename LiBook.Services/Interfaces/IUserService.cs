using LiBook.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace LiBook.Services.Interfaces
{
    public interface IUserService
    {
        UserProfile GetUserProfile(ClaimsPrincipal principal);
        IEnumerable<UserProfile> GetUserProfiles();
        void Update(UserProfile user);
        void Delete(string user);
    }
}
