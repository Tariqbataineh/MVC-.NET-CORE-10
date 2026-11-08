using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityCafeteriaManagement.Data;
using UniversityCafeteriaManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityCafeteriaManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeteriaDbContext db;

        public ProductsController(CafeteriaDbContext context)
        {
            db = context;
        }

        // Display Products
        [HttpGet]
        public IActionResult Index()
        {
            var products = db.Products.ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(products);
        }

        // Open Create Page
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View();
        }


        // Save New Product
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, decimal price, int quantity,
                                    int categoryId, int supplierId)
        {
            if (string.IsNullOrWhiteSpace(name) || price < 0 || quantity < 0)
            {
                TempData["Error"] = "Invalid product information.";

                return RedirectToAction("Create");
            }

            var category = db.Categories.Find(categoryId);
            var supplier = db.Suppliers.Find(supplierId);

            if (category == null || supplier == null)
            {
                TempData["Error"] = "Invalid category or supplier.";

                return RedirectToAction("Create");
            }

            Product product = new Product();

            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.CategoryId = categoryId;
            product.SupplierId = supplierId;

            db.Products.Add(product);

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // Open Edit Page
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(product);
        }


        // Save Product Changes
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string name, decimal price, int quantity,
                                  int categoryId, int supplierId)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(name) || price < 0 || quantity < 0)
            {
                TempData["Error"] = "Invalid product information.";

                return RedirectToAction("Edit", new { id = id });
            }

            var category = db.Categories.Find(categoryId);
            var supplier = db.Suppliers.Find(supplierId);

            if (category == null || supplier == null)
            {
                TempData["Error"] = "Invalid category or supplier.";

                return RedirectToAction("Edit", new { id = id });
            }

            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.CategoryId = categoryId;
            product.SupplierId = supplierId;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // Delete Product
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var orderItem = db.PurchaseOrderItems
                .FirstOrDefault(item => item.ProductId == id);

            if (orderItem != null)
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