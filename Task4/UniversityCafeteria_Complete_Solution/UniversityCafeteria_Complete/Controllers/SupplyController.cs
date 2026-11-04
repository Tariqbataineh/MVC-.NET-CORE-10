using Microsoft.AspNetCore.Mvc;
namespace UniversityCafeteria.Controllers;
public class SupplyController : Controller
{
    [HttpGet] public IActionResult Index() { ViewBag.Categories = new[] { "Drinks", "Food", "Snacks", "Hot Drinks", "Cold Drinks" }; return View(); }
    [HttpGet] public IActionResult Products() { ViewData["PageTitle"] = "Cafeteria Products"; ViewData["ManagerName"] = "Ahmad Ali"; ViewData["ProductCount"] = 5; ViewData["Products"] = new[] { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate" }; return View(); }
    [HttpGet] public IActionResult Suppliers() { ViewBag.PageTitle = "Our Suppliers"; ViewBag.SupplierCount = 3; ViewBag.Suppliers = new[] { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" }; return View(); }
    [HttpGet] public IActionResult CreateRequest() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SubmitRequest()
    {
        var name = Request.Form["EmployeeName"].ToString();
        if (string.IsNullOrWhiteSpace(name)) { ViewBag.Error = "Please enter your name."; return View(); }
        TempData["Message"] = "Supply request submitted successfully!";
        return RedirectToAction(nameof(About));
    }
    [HttpGet] public IActionResult About() => View();
    [HttpGet] public IActionResult CheckQuantity(int quantity)
    {
        var size = quantity < 20 ? "Small Order" : quantity <= 50 ? "Medium Order" : "Large Order";
        return Content(size);
    }
}
