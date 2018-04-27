using System.Collections.Generic;
using System.Threading.Tasks;
using Incognito.Data;
using Incognito.Models;
using Incognito.Models.AccountViewModels;
using Incognito.Models.AdminViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Incognito.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly UserContext _userContext;

        public AdminController (
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            UserContext userContext
            )
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _userContext = userContext;
        }

        public IActionResult Users(string role)
        {
            switch (role)
            {
                case "Admin": return View(_userRepository.GetAllAdmins());
                case "Moderator": return View(_userRepository.GetAllModerators());
                default: return View(_userRepository.GetAllUsers());
            }
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel addUserViewModel)
        {
            if (!ModelState.IsValid) return View(addUserViewModel);

            var user = new ApplicationUser()
            {
                FirstName = addUserViewModel.FirstName,
                LastName = addUserViewModel.LastName,
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
            };

            _logger.LogInformation("User created a new account with password.");
            var result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Moderator");

                var profile = new UserProfile
                {
                    UserId = user.Id
                };

                _userContext.Add(profile);
                await _userContext.SaveChangesAsync();

                _logger.LogInformation("User created with Moderator role.");
                return RedirectToAction("Users");
            }

            AddErrors(result);
            return View(addUserViewModel);
        }

        public IActionResult Roles()
        {
            var model = new RoleVM
            {
                IdentityRole = _userRepository.GetRoles(),
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
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return View(viewModel);
            var model = new RoleVM
            {
                IdentityRole = _userRepository.GetRoles(),
            };
            return View(model);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return BadRequest();
            var editRoleVM = new EditRoleVM
            {
                Id = role.Id,
                Name = role.Name,
                Users = new List<string>()
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editRoleVM.Users.Add(user.UserName);
                }
            }
            return View(editRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM editRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role == null) return RedirectToAction("Roles", _roleManager.Roles);

            role.Name = editRoleViewModel.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return RedirectToAction("Roles");

            return View(editRoleViewModel);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
