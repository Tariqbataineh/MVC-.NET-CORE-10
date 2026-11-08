using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Data;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Suppliers = _context.Suppliers.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) ||
                product.Price < 0 || product.Quantity < 0)
            {
                ViewBag.Error = "Please enter valid product information.";
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Suppliers = _context.Suppliers.ToList();

                return View();
            }

            if (_context.Categories.Find(product.CategoryId) == null ||
                _context.Suppliers.Find(product.SupplierId) == null)
            {
                ViewBag.Error = "Please select a valid category and supplier.";
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Suppliers = _context.Suppliers.ToList();

                return View();
            }

            _context.Products.Add(product);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}