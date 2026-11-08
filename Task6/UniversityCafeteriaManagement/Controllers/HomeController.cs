using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityCafeteriaManagement.Models;

namespace UniversityCafeteriaManagement.Controllers
{
    public class HomeController : Controller
    {
        // Home Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Privacy Page
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        // Error Page
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel error = new ErrorViewModel();

            error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(error);
        }
    }
}