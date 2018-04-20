using Incognito.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Incognito.Data
{
    public interface IUserRepository
    {
        bool CheckUserExist(string username);

        UserProfile GetUserByUsername(string username);

        UserProfile GetUserById(string userId);

        ProfileCardService GetCardService(string userId);

        ProfileCardService GetCardServiceWithSocial(string username);

        IQueryable<ApplicationUser> GetAllUsers();

        IQueryable<ApplicationUser> GetAllModerators();

        IQueryable<ApplicationUser> GetAllAdmins();

    }
}
