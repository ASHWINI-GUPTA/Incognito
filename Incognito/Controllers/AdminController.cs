using Incognito.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminController (
            IMessageRepository messageRepository,
            IUserRepository userRepository
            )
        {
            this.userRepository = userRepository;
        }

        public IActionResult Users(string role)
        {
            switch (role)
            {
                case "Admin": return View(userRepository.GetAllAdmins());
                case "Moderator": return View(userRepository.GetAllModerators());
                default: return View(userRepository.GetAllUsers());
            }
        }
    }
}
