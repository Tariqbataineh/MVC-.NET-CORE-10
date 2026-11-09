using Microsoft.AspNetCore.Mvc;
using University_Cafeteria_Management_System_.Data;
using University_Cafeteria_Management_System_.Models;

namespace University_Cafeteria_Management_System_.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext db;

        public EmployeeController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            db.Employees.Update(employee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
                return NotFound();

            db.Employees.Remove(employee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}