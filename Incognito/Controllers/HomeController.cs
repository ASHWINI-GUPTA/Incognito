using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Incognito.Models;

namespace Incognito.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult UserNotFound()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
