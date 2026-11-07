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

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View();
            }

            Category category = new Category
            {
                Name = name,
                Description = description
            };

            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}