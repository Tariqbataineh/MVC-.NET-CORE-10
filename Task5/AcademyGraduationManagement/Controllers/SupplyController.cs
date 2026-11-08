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

            // Check Selected Offers
            var certificate = db.Offers.Find(certificateOffer.Value);
            var hospitality = db.Offers.Find(hospitalityOffer.Value);
            var gift = db.Offers.Find(giftOffer.Value);

            if (certificate == null || hospitality == null || gift == null)
            {
                TempData["Error"] = "Invalid offers.";

                return RedirectToAction("Index");
            }

            if (certificate.CategoryId != 1 || hospitality.CategoryId != 2 || gift.CategoryId != 3)
            {
                TempData["Error"] = "Invalid categories.";

                return RedirectToAction("Index");
            }

            // Save List in Session
            List<int> selectedOffers = new List<int>();

            selectedOffers.Add(certificateOffer.Value);
            selectedOffers.Add(hospitalityOffer.Value);
            selectedOffers.Add(giftOffer.Value);

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
                int id;

                if (!int.TryParse(value, out id))
                {
                    return RedirectToAction("Index");
                }

                selectedOffers.Add(id);
            }

            // Get Offers from Database
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

            // Calculate Discount
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

            // Final Total
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