using Incognito.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers
{
    [Authorize(Policy = "RequireModeratorRole")]
    public class ModeratorController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public ModeratorController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IActionResult Report()
        {
            return View(_messageRepository.GetReportedMessage());
        }
    }
}
