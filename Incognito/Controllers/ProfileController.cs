using Incognito.Data;
using Incognito.Models;
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
        private readonly UserContext userContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;

        public ProfileController(
            UserContext userContext,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            this.userContext = userContext;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var profile = userContext.Profiles
                            .Include(u => u.User)
                            .Single(u => u.UserId == userId);

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int Id, UserProfile profile)
        {
            var currentUserId = userManager.GetUserId(User);

            var profileToUpdate = userContext.Profiles
                .Single(c => c.UserId == currentUserId);

            var userToUpdate = userContext.Users
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


                userContext.Update(userToUpdate);
                userContext.Update(profileToUpdate);

                await userContext.SaveChangesAsync();

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
