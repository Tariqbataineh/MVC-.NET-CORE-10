using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        // PART 4: Pass a collection from Controller to View.
        [HttpGet]
        public IActionResult Index()
        {
            string[] categories = { "Drinks", "Food", "Snacks", "Hot Drinks", "Cold Drinks" };
            ViewBag.Categories = categories;
            return View();
        }

        // PART 2: ViewData.
        [HttpGet]
        public IActionResult Products()
        {
            ViewData["PageTitle"] = "Cafeteria Products";
            ViewData["ManagerName"] = "Ahmad Ali";
            ViewData["ProductCount"] = 5;
            string[] products = { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate" };
            ViewData["Products"] = products;
            return View();
        }

        // PART 3: ViewBag.
        [HttpGet]
        public IActionResult Suppliers()
        {
            ViewBag.PageTitle = "Our Suppliers";
            ViewBag.SupplierCount = 3;
            string[] suppliers = { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            ViewBag.Suppliers = suppliers;
            return View();
        }

        // PART 5: GET displays the form.
        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }

        // PART 5 & 6: POST reads Request.Form and redirects upon success.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRequest()
        {
            string employeeName = Request.Form["EmployeeName"].ToString();
            string productName = Request.Form["ProductName"].ToString();
            string quantityText = Request.Form["Quantity"].ToString();
            string requiredDateText = Request.Form["RequiredDate"].ToString();

            if (string.IsNullOrWhiteSpace(employeeName))
            {
                ViewBag.Error = "Employee Name is required.";
                return View();
            }

            string[] allowedProducts = { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate" };
            if (!allowedProducts.Contains(productName) ||
                !int.TryParse(quantityText, out int quantity) || quantity <= 0 ||
                !DateTime.TryParseExact(requiredDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out _))
            {
                ViewBag.Error = "Please enter valid request details.";
                return View();
            }

            TempData["Message"] = "Supply request submitted successfully!";
            return RedirectToAction("About");
        }

        // PART 6: View displays TempData["Message"] if present.
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        // OPTIONAL BONUS: Dynamic quantity classification.
        [HttpGet]
        public IActionResult CheckQuantity(int quantity)
        {
            if (quantity < 20) return Content("Small Order");
            if (quantity <= 50) return Content("Medium Order");
            return Content("Large Order");
        }
    }
}
