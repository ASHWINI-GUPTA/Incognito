using Incognito.Data;
using Incognito.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers
{
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

    }
}
