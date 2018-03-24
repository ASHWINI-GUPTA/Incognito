using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incognito.Data;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace Incognito.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserDbContext _context;

        public UserController(ApplicationUserDbContext context)
        {
            _context = context;
        }

        public ActionResult Public(string username)
        {
            var lookUpUser = username;

            //var userFound = _context.Users.Where(u => u.)

            //ViewData["User"] = username;
            return View();
        }
    }
}
