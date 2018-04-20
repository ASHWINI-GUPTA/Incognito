using System.Collections.Generic;
using System.Threading.Tasks;
using Incognito.Data;
using Incognito.Models;
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
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController (
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            this.userRepository = userRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return BadRequest();
            var editRoleVM = new EditRoleVM
            {
                Id = role.Id,
                Name = role.Name,
                Users = new List<string>()
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    editRoleVM.Users.Add(user.UserName);
                }
            }

            return View(editRoleVM);
        }
    }
}
