using Incognito.Models;
using System.Collections.Generic;
using System.Linq;

namespace Incognito.Data
{
    public interface IUserRepository
    {
        bool CheckUserExist(string username);

        UserProfile GetUserByUsername(string username);

        UserProfile GetUserById(string userId);

        IEnumerable<ApplicationUser> GetAllUser();

        ProfileCardService GetCardService(string userId);

        ProfileCardService GetCardServiceWithSocial(string username);
    }
}
