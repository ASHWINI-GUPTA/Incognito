using AutoMapper;
using Incognito.Data;
using Incognito.Models;
using Incognito.Models.ProfileViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Incognito.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserContext userContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public ProfileController(
            UserContext userContext,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.userContext = userContext;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var user = userRepository.GetUserById(userId);

            var model = new UserProfileVM
            {
                UserName = user.User.UserName,
                Email = user.User.Email,
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                Twitter = user.Twitter,
                CompanyName = user.CompanyName,
                JobTitle = user.JobTitle,
                AfterWords = user.AfterWords
            };

            return base.View(new ProfileVM
            {
                UserProfileVM = model,
                ProfileCardService = userRepository.GetCardService(userId)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileVM profile)
        {
            var user = userRepository.GetUserById(userManager.GetUserId(User));

            if (user.UserId != userManager.GetUserId(User)) return BadRequest();

            if (!ModelState.IsValid) return View(profile);

            try
            {
                user.User.FirstName = profile.UserProfileVM.FirstName;
                user.User.LastName = profile.UserProfileVM.LastName;

                user.CompanyName = profile.UserProfileVM.CompanyName;
                user.JobTitle = profile.UserProfileVM.JobTitle;
                user.AfterWords = profile.UserProfileVM.AfterWords;
                user.Twitter = profile.UserProfileVM.Twitter;

                userContext.Update(user);
                await userContext.SaveChangesAsync();

                return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "User", action = "Index" }));
            }
            catch
            {
                return View(profile);
            }
        }
    }
}
