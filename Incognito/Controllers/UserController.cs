using Incognito.Data;
using Incognito.Models;
using Incognito.Models.ProfileViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Incognito.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IMessageRepository messageRepository,
            IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        //Index Page
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);

            return base.View(new ProfileVM
            {
                Messages = messageRepository.GetUserMessages(userId),
                ProfileCardService = userRepository.GetCardService(userId)
            });
        }

        //Archive Page
        public IActionResult Archive()
        {
            return base.View(messageRepository.GetUserArchives(userManager.GetUserId(User)));
        }

        //Get current logged in user's identifier
        private Task<ApplicationUser> GetCurrentUserAsync() =>
                userManager.GetUserAsync(HttpContext.User);
    }
}
