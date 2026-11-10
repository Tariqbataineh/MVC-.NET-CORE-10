using Microsoft.AspNetCore.Mvc;
using University_Cafeteria_Management_System_.Data;
using University_Cafeteria_Management_System_.Models;

namespace University_Cafeteria_Management_System_.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext context)
        {
            db = context;
        }

        // Index
        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // Create GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Edit GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);

            return View(category);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = db.Categories.Find(id);

          

            return View(category);
        }

        // Delete POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
                return NotFound();

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}