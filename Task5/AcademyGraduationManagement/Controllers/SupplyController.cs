using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyGraduationManagement.Data;

namespace AcademyGraduationManagement.Controllers
{
    public class SupplyController : Controller
    {
        private readonly AppDbContext db;

        public SupplyController(AppDbContext context)
        {
            db = context;
        }

        // Display Offers
        [HttpGet]
        public IActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Offers).ToList();

            return View(categories);
        }


        // Save Offers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveOffers(int? certificateOffer, int? hospitalityOffer, int? giftOffer)
        {
            if (certificateOffer == null || hospitalityOffer == null || giftOffer == null)
            {
                TempData["Error"] = "Please select all offers.";
                return RedirectToAction("Index");
            }

            bool c = db.Offers.Any(o => o.Id == certificateOffer && o.CategoryId == 1);
            bool h = db.Offers.Any(o => o.Id == hospitalityOffer && o.CategoryId == 2);
            bool g = db.Offers.Any(o => o.Id == giftOffer && o.CategoryId == 3);

            if (!c || !h || !g)
            {
                TempData["Error"] = "Invalid offer selection.";
                return RedirectToAction("Index");
            }

            List<int> selectedOffers = new List<int>
            {
                certificateOffer.Value,
                hospitalityOffer.Value,
                giftOffer.Value
            };

            string offers = string.Join(",", selectedOffers);

            HttpContext.Session.SetString("SelectedOffers", offers);

            return RedirectToAction("Review");
        }


        // Review Offers
        [HttpGet]
        public IActionResult Review()
        {
            string? offers = HttpContext.Session.GetString("SelectedOffers");

            if (string.IsNullOrEmpty(offers))
            {
                return RedirectToAction("Index");
            }

            string[] values = offers.Split(',');

            if (values.Length != 3)
            {
                return RedirectToAction("Index");
            }

            List<int> selectedOffers = new List<int>();

            foreach (string value in values)
            {
                if (!int.TryParse(value, out int id))
                {
                    return RedirectToAction("Index");
                }

                selectedOffers.Add(id);
            }

            var certificate = db.Offers.Find(selectedOffers[0]);
            var hospitality = db.Offers.Find(selectedOffers[1]);
            var gift = db.Offers.Find(selectedOffers[2]);

            if (certificate == null || hospitality == null || gift == null)
            {
                return RedirectToAction("Index");
            }

            if (certificate.CategoryId != 1 || hospitality.CategoryId != 2 || gift.CategoryId != 3)
            {
                return RedirectToAction("Index");
            }

            // Calculate Prices
            decimal certificateTotal = certificate.Quantity * certificate.UnitPrice;
            decimal hospitalityTotal = hospitality.Quantity * hospitality.UnitPrice;
            decimal giftTotal = gift.Quantity * gift.UnitPrice;

            decimal total = certificateTotal + hospitalityTotal + giftTotal;

            // Discount
            decimal discount = 0;

            if (total > 500)
            {
                discount = 15;
            }
            else if (total > 300)
            {
                discount = 10;
            }

            decimal discountAmount = total * discount / 100;

            decimal finalTotal = total - discountAmount;


            // Certificates
            ViewBag.CertificateSupplier = certificate.SupplierName;
            ViewBag.CertificateOfferName = certificate.OfferName;
            ViewBag.CertificateQuantity = certificate.Quantity;
            ViewBag.CertificatePrice = certificate.UnitPrice;
            ViewBag.CertificateTotal = certificateTotal;

            // Hospitality
            ViewBag.HospitalitySupplier = hospitality.SupplierName;
            ViewBag.HospitalityOfferName = hospitality.OfferName;
            ViewBag.HospitalityQuantity = hospitality.Quantity;
            ViewBag.HospitalityPrice = hospitality.UnitPrice;
            ViewBag.HospitalityTotal = hospitalityTotal;

            // Gifts
            ViewBag.GiftSupplier = gift.SupplierName;
            ViewBag.GiftOfferName = gift.OfferName;
            ViewBag.GiftQuantity = gift.Quantity;
            ViewBag.GiftPrice = gift.UnitPrice;
            ViewBag.GiftTotal = giftTotal;

            // Total
            ViewBag.TotalPrice = total;
            ViewBag.DiscountPercentage = discount;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.FinalTotal = finalTotal;

            return View();
        }


        // About Page
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}