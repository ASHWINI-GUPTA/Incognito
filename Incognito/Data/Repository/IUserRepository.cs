using Incognito.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        IQueryable<IdentityRole> GetRoles();

        Task<bool> IsMember(string username);
    }
}
