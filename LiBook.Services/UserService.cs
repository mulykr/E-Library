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

        public IEnumerable<UserProfile> GetUserProfiles()
        {
            return _userRepository.GetList();
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
