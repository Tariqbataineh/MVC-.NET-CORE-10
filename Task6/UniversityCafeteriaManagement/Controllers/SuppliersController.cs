using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityCafeteriaManagement.Data;
using UniversityCafeteriaManagement.Models;

namespace UniversityCafeteriaManagement.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly CafeteriaDbContext db;

        public SuppliersController(CafeteriaDbContext context)
        {
            db = context;
        }

        // Display Suppliers
        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = db.Suppliers.ToList();

            return View(suppliers);
        }

        // Open Create Page
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save New Supplier
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string email, string? phone)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Please enter supplier name and email.";

                return View();
            }

            // Create Supplier
            Supplier supplier = new Supplier();

            supplier.Name = name;
            supplier.Email = email;
            supplier.Phone = phone;

            // Save to Database
            db.Suppliers.Add(supplier);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}