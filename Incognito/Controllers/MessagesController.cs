using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Incognito.Data;
using Incognito.Models;

namespace Incognito.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MessageContext context;
        private readonly IMessageRepository messageRepository;

        public MessagesController(MessageContext context, IMessageRepository messageRepository)
        {
            this.context = context;
            this.messageRepository = messageRepository;
        }

        // GET all the Messages
        public IActionResult Index()
        {
            return View(messageRepository.GetAllMessages());
        }

        // GET: Messages Details
        public IActionResult Details(int id)
        {
            var messageFound = messageRepository.CheckMessageExists(id);

            if (!messageFound) return NotFound();

            return View(messageRepository.GetMessage(id));
        }
    }
}
