using Incognito.Data;
using Incognito.Models;
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
        private ApplicationUserDbContext _userContext;
        private MessageContext _messageContext;
        private UserManager<ApplicationUser> _userManager;

        public UserController(
            ApplicationUserDbContext userContext,
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

            var userMassages = await _messageContext.Messages
                .Where(u => u.RecevierId == userId)
                .ToListAsync();

            return View(userMassages);
        }

        //Get current logged in user's identifier
        private Task<ApplicationUser> GetCurrentUserAsync() => 
            _userManager.GetUserAsync(HttpContext.User);
    }
}
