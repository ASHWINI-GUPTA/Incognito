using Incognito.Data;
using Incognito.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Incognito.Controllers.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/message")]
    public class MessageAPIController : Controller
    {
        private readonly MessageContext messageContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMessageRepository messageRepository;

        public MessageAPIController(
            MessageContext messageContext,
            UserManager<ApplicationUser> userManager,
            IMessageRepository messageRepository )
        {
            this.messageContext = messageContext;
            this.userManager = userManager;
            this.messageRepository = messageRepository;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = userManager.GetUserId(User);
            messageRepository.GetMessageByUserId(id, userId).IsDeleted = true;
            messageContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Archive(int id)
        {
            var userId = userManager.GetUserId(User);
            var message = messageRepository.GetMessageByUserId(id, userId);
            message.IsArchived = message.IsArchived == false;
            messageContext.SaveChanges();

            return Ok();
        }

        [HttpPost("report/{id}")]
        public async Task<IActionResult> Report(int id, string reason)
        {
            var userId = userManager.GetUserId(User);
            var message = messageContext.Messages
                .Single(c => c.Id == id && c.RecevierId == userId);

            if (message.IsReported) return Ok();

            message.IsReported = true;

            var report = new ReportMessage
            {
                UserId = userId,
                MessageId = id,
                Reason = reason,
                ReportTime = DateTime.UtcNow
            };
            messageContext.Add(report);
            await messageContext.SaveChangesAsync();

            return Ok();
        }
    }
}
