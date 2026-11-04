using Microsoft.AspNetCore.Mvc;
namespace UniversityCafeteria.Controllers;
public class CafeteriaController : Controller
{
    [HttpGet] public IActionResult Index() => View();
    [HttpGet] public IActionResult Products() => View();
    [HttpGet] public IActionResult Suppliers() => View();
    [HttpGet] public IActionResult SupplyStatus() => View();
}
