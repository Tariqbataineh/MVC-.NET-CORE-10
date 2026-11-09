using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_Cafeteria_Management_System_.Data;
using University_Cafeteria_Management_System_.Models;

namespace University_Cafeteria_Management_System_.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db;

        public OrderController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var orders = db.Orders
                .Include(x => x.Employee)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = db.Employees.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = db.Orders.Find(id);

            if (order == null)
                return NotFound();

            ViewBag.Employees = db.Employees.ToList();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order order)
        {
            db.Orders.Update(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = db.Orders
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var order = db.Orders.Find(id);

            if (order == null)
                return NotFound();

            db.Orders.Remove(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}