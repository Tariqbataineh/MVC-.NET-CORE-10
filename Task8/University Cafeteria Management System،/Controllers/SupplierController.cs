using Microsoft.AspNetCore.Mvc;
using University_Cafeteria_Management_System_.Data;
using University_Cafeteria_Management_System_.Models;

namespace University_Cafeteria_Management_System_.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext db;

        public SupplierController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier)
        {
            db.Suppliers.Update(supplier);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
                return NotFound();

            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}