using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteriaManagement.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}