using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityCafeteriaManagement.Data;
using UniversityCafeteriaManagement.Models;

namespace UniversityCafeteriaManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeteriaDbContext db;

        public ProductsController(CafeteriaDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, decimal price, int quantity,
                                    int categoryId, int supplierId)
        {
            if (string.IsNullOrWhiteSpace(name) || price < 0 || quantity < 0 ||
                !db.Categories.Any(x => x.Id == categoryId) ||
                !db.Suppliers.Any(x => x.Id == supplierId))
            {
                TempData["Error"] = "Invalid product information.";
                return RedirectToAction("Create");
            }

            Product product = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string name, decimal price, int quantity,
                                  int categoryId, int supplierId)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(name) || price < 0 || quantity < 0 ||
                !db.Categories.Any(x => x.Id == categoryId) ||
                !db.Suppliers.Any(x => x.Id == supplierId))
            {
                TempData["Error"] = "Invalid product information.";
                return RedirectToAction("Edit", new { id });
            }

            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.CategoryId = categoryId;
            product.SupplierId = supplierId;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            if (db.PurchaseOrderItems.Any(x => x.ProductId == id))
            {
                TempData["Error"] = "Product is used in a purchase order.";
                return RedirectToAction("Index");
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}