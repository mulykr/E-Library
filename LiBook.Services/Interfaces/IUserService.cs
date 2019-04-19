using LiBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace LiBook.Services.Interfaces
{
    public interface IUserService
    {
        UserProfile GetUserProfile(ClaimsPrincipal principal);
        UserProfile GetUserProfile(string id);
        string GetUserProfileId(ClaimsPrincipal principal);
        IEnumerable<UserProfile> GetUserProfiles();
        void ChangeFirstName(UserProfile user, string firstName);
        void ChangeFirstName(ClaimsPrincipal principal, string firstName);
        void ChangeSecondName(UserProfile user, string lastName);
        void ChangeSecondName(ClaimsPrincipal principal, string secondName);
        void Update(UserProfile user);
        void Delete(string user);
    }
}
