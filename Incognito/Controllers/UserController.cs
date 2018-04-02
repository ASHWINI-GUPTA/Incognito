using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incognito.Data;
using Incognito.Models;
using Incognito.Models.MessageViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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

        [HttpGet]
        public ActionResult Public(string username)
        {
            var lookUpUser = username;
            var userFound = _userContext.Users
                .FirstOrDefault(u => u.UserName == lookUpUser);

            if (userFound != null)
            {
                var userId = userFound.Id;
                var userName = $"{userFound.FirstName} {userFound.LastName}";
                ViewData["User"] = userName;
                ViewData["receiverId"] = userId;

                return View();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Public(MessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var senderId = _userManager.GetUserId(User);

                var message = new Message
                {
                    RecevierId =viewModel.ReceiverId,
                    Text = viewModel.Text,
                    SentTime = DateTime.Now,
                };
                if (senderId == null)
                {
                    message.SenderId = "Not Provided";
                } else
                {
                    message.SenderId = senderId;
                }

                _messageContext.Add(message);
                await _messageContext.SaveChangesAsync();
                return Ok();
            }
            return View(viewModel);
        }
    }
}
