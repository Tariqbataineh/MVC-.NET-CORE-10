using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityCafeteriaManagement.Data;
using UniversityCafeteriaManagement.Models;

namespace UniversityCafeteriaManagement.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CafeteriaDbContext db;

        public CategoriesController(CafeteriaDbContext context)
        {
            db = context;
        }

        // Display Categories
        [HttpGet]
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();

            return View(categories);
        }

        // Open Create Page
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save Category
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View();
            }

            Category category = new Category();

            category.Name = name;
            category.Description = description;

            db.Categories.Add(category);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}