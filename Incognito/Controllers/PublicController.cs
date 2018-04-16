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
        public ActionResult Index(string username)
        {
            // Redirect to About if no username is given
            if (username == null) return RedirectToAction("About", new RouteValueDictionary(
                        new { controller = "Home", action = "About" }));

            var userCheck = userRepository.UserExist(username);

            // Returning NotFound is no User found
            if (!userCheck) return RedirectToAction("UserNotFound", new RouteValueDictionary(
                        new { controller = "Home", action = "UserNotFound" }));

            var user = userRepository.GetUserByUsername(username);

            var viewModel = new PublicVM
            {
                PublicProfile = mapper.Map<UserProfile>(user),
                PublicMessage = new MessageVM {
                    ReceiverId = user.UserId
                },
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PublicVM viewModel)
        {
            //var user = viewModel.PublicProfile.User;

            if (ModelState.IsValid)
            {
                var senderId = userManager.GetUserId(User);

                var message = new Message
                {
                    RecevierId =viewModel.PublicMessage.ReceiverId,
                    Text = viewModel.PublicMessage.Text,
                    SentTime = DateTime.UtcNow,
                };
                if (senderId == null)
                {
                    message.SenderId = "Not Provided";
                } else
                {
                    message.SenderId = senderId;
                }
                messageContext.Add(message);
                await messageContext.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }

            //var vModel = new PublicVM
            //{
            //    PublicProfile = mapper.Map<UserProfile>(viewModel.PublicProfile),
            //    PublicMessage = new MessageVM
            //    {
            //        ReceiverId = user.Id
            //    },
            //};

            return View(viewModel);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
