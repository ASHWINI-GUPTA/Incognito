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
        private UserContext _userContext;
        private MessageContext _messageContext;
        private UserManager<ApplicationUser> _userManager;

        public UserController(
            UserContext userContext,
            MessageContext messageContext,
            UserManager<ApplicationUser> userManager)
        {
            _userContext = userContext;
            _messageContext = messageContext;
            _userManager = userManager;
        }

        //Index Page
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userId = _userManager.GetUserId(User);
            ViewData["User"] = $"{user.FirstName} {user.LastName}";

            var userMessages = await _messageContext.Messages
                .Where(u => u.RecevierId == userId && !u.IsDeleted && !u.IsArchived)
                .OrderByDescending(d => d.SentTime)
                .ToListAsync();

            var profile = _userContext.Profiles
                .Include(u => u.User)
                .Single(u => u.UserId == userId);

            var viewModel = new UserViewModel
            {
                Messages = userMessages,
                Profile = profile
            };

            return View(viewModel);
        }

        //Archive Page
        public async Task<IActionResult> Archive()
        {
            var userId = _userManager.GetUserId(User);

            var userArchivedMessages = await _messageContext.Messages
                .Where(u => u.RecevierId == userId && u.IsArchived)
                .OrderByDescending(d => d.SentTime)
                .ToListAsync();

            return View(userArchivedMessages);
        }

        //Get current logged in user's identifier
        private Task<ApplicationUser> GetCurrentUserAsync() => 
            _userManager.GetUserAsync(HttpContext.User);
    }
}
