using System.Collections.Generic;
using System.Linq;
using Incognito.Models;
using Microsoft.EntityFrameworkCore;

namespace Incognito.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public bool CheckUserExist(string username)
        {
            return userContext.Users
                .Any(u => u.UserName == username);
        }

        public UserProfile GetUserByUsername(string username)
        {
            var userLookup = userContext.Profiles.Include(u => u.User)
                .FirstOrDefault(u => u.User.UserName == username);

            return userLookup;
        }

        public UserProfile GetUserById(string userId)
        {
            var userLookup = userContext.Profiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.UserId == userId);

            return userLookup;
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return userContext.Users.AsEnumerable();
        }

        public ProfileCardService GetCardService(string userId)
        {
            var card = GetUserById(userId);

            return new ProfileCardService
            {
                FirstName = card.User.FirstName,
                LastName = card.User.LastName,
                Company = card.CompanyName
            };
        }

        public ProfileCardService GetCardServiceWithSocial(string username)
        {
            var user = GetUserByUsername(username);

            return new ProfileCardService
            {
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                Company = user.CompanyName,
                Twitter = user.Twitter
            };
        }
    }
}
