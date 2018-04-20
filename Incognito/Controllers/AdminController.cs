using System.Threading.Tasks;
using Incognito.Data;
using Incognito.Models.AdminViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController (
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userRepository = userRepository;
            this.roleManager = roleManager;
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

        public IActionResult Roles()
        {
            var model = new RoleVM
            {
                IdentityRole = userRepository.GetRoles(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(RoleVM viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var role = new IdentityRole
            {
                Name = viewModel.AddRole.Name
            };
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded) return View(viewModel);
            var model = new RoleVM
            {
                IdentityRole = userRepository.GetRoles(),
            };
            return View(model);
        }
    }
}
