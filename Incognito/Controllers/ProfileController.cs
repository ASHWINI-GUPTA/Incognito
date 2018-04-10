using Incognito.Data;
using Incognito.Models;
using Incognito.Models.ProfileViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Incognito.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserContext _userContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserContext userContext, UserManager<ApplicationUser> userManager)
        {
            _userContext = userContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var profile = _userContext.Profiles
                            .Include(u => u.User)
                            .SingleOrDefault(u => u.UserId == userId);

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int Id, Profile profile)
        {
            var currentUserId = _userManager.GetUserId(User);

            var profileToUpdate = _userContext.Profiles
                .Single(c => c.UserId == currentUserId);

            var userToUpdate = _userContext.Users
                .Single(c => c.Id == profileToUpdate.UserId);

            if (userToUpdate.Id != currentUserId)
            {
                return BadRequest();
            }

            try
            {
                userToUpdate.FirstName = profile.User.FirstName;
                userToUpdate.LastName = profile.User.LastName;

                profileToUpdate.CompanyName = profile.CompanyName;
                profileToUpdate.JobTitle = profile.JobTitle;
                profileToUpdate.AfterWords = profile.AfterWords;
                profileToUpdate.Twitter = profile.Twitter;


                _userContext.Update(userToUpdate);
                _userContext.Update(profileToUpdate);

                await _userContext.SaveChangesAsync();

                return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "User", action = "Index" }));

            }
            catch
            {
                ViewData["Error"] = "Error occur while updating the database.";
                return View(profile);
            }
        }
    }
}
