using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyGraduationManagement.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public string SupplierName { get; set; } = "";

        public string OfferName { get; set; } = "";

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}