using Incognito.Data;
using Incognito.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Incognito.Controllers.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/message/{id}")]
    public class MessageController : Controller
    {
        private readonly MessageContext _messageContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(MessageContext messageContext, UserManager<ApplicationUser> userManager)
        {
            _messageContext = messageContext;
            _userManager = userManager;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var message = _messageContext.Messages
                .Single(c => c.Id == id && c.RecevierId == userId);
            message.IsDeleted = true;
            _messageContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Archive(int id)
        {
            var userId = _userManager.GetUserId(User);
            var message = _messageContext.Messages
                .Single(c => c.Id == id && c.RecevierId == userId);
            message.IsArchived = true;
            _messageContext.SaveChanges();

            return Ok();
        }
    }
}
