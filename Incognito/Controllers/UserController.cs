using Incognito.Data;
using Incognito.Models;
using Incognito.Models.ProfileViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var card = userRepository.GetUserById(userId);

            var profile = new ProfileCardService
            {
                FirstName = card.User.FirstName,
                LastName = card.User.LastName,
                Company = card.CompanyName
            };

            var viewModel = new UserViewModel
            {
                Messages = messageRepository.GetUserMessages(userId),
                ProfileCardService  = profile
                
            };
            return View(viewModel);
        }

        //Archive Page
        public IActionResult Archive()
        {
            var userId = userManager.GetUserId(User);
            return View(messageRepository.GetUserArchives(userId));
        }

        //Get current logged in user's identifier
        private Task<ApplicationUser> GetCurrentUserAsync() =>
            userManager.GetUserAsync(HttpContext.User);
    }
}
