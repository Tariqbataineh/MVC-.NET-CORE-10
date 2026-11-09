using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Data;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name) ||
                string.IsNullOrWhiteSpace(supplier.Email))
            {
                ViewBag.Error = "Please enter supplier name and email.";
                return View();
            }

            _context.Suppliers.Add(supplier);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}