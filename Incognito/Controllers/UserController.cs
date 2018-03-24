using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Public(string username)
        {
            ViewData["User"] = username;

            return View();
        }
    }
}
