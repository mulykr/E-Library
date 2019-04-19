using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using LiBook.Services.Extensions.Identity;

namespace LiBook.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserProfile> _userRepository;

        public UserService(IRepository<UserProfile> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserProfile GetUserProfile(ClaimsPrincipal principal)
        {
            return _userRepository.Get(principal.GetUserId());
        }

        public UserProfile GetUserProfile(string id)
        {
            return _userRepository.Get(id);
        }

        public string GetUserProfileId(ClaimsPrincipal principal)
        {
            return principal.GetUserId();
        }

        public IEnumerable<UserProfile> GetUserProfiles()
        {
            return _userRepository.GetList();
        }

        public void ChangeFirstName(UserProfile user, string firstName)
        {
            user.FirstName = firstName;
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void ChangeFirstName(ClaimsPrincipal principal, string firstName)
        {
            var user = GetUserProfile(principal);
            ChangeFirstName(user, firstName);
            _userRepository.Save();
        }

        public void ChangeSecondName(UserProfile user, string lastName)
        {
            user.LastName = lastName;
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void ChangeSecondName(ClaimsPrincipal principal, string secondName)
        {
            var user = GetUserProfile(principal);
            ChangeSecondName(user, secondName);
            _userRepository.Save();
        }

        public void Update(UserProfile user)
        {
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void Delete(string id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
        }
    }
}
