using System;
using System.Threading.Tasks;
using AutoMapper;
using Incognito.Data;
using Incognito.Models;
using Incognito.Models.MessageViewModel;
using Incognito.Models.ProfileViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Incognito.Controllers
{
    public class PublicController : Controller
    {
        private UserContext userContext;
        private MessageContext messageContext;
        private UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public PublicController(
            UserContext userContext,
            MessageContext messageContext,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.userContext = userContext;
            this.messageContext = messageContext;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string username)
        {
            // Redirect to About if no username is given
            if (username == null) return RedirectToAction("About", new RouteValueDictionary(
                        new { controller = "Home", action = "About" }));

            var userCheck = userRepository.CheckUserExist(username);
            // Returning NotFound if no User found and user is not in Member role
            if (!userCheck) return RedirectToAction("UserNotFound", new RouteValueDictionary(
                        new { controller = "Home", action = "UserNotFound" }));

            var isMember = await userRepository.IsMember(username);
            if (!isMember) return RedirectToAction("UserNotFound", new RouteValueDictionary(
                        new { controller = "Home", action = "UserNotFound" }));

            return base.View( new PublicVM
            {
                PublicMessage = new MessageVM
                {
                    ReceiverId = userRepository.GetUserByUsername(username).UserId
                },
                ProfileCardService = userRepository.GetCardServiceWithSocial(username)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PublicVM viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var message = new Message
            {
                RecevierId = viewModel.PublicMessage.ReceiverId,
                Text = viewModel.PublicMessage.Text,
                SentTime = DateTime.UtcNow,
                SenderId = userManager.GetUserId(User) ?? "Not Provided"
            };
            messageContext.Add(message);
            await messageContext.SaveChangesAsync();
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
