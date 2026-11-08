using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Category");
        }
    }
}