using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityCafeteriaManagement.CodeFirstModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public Category Category { get; set; } = null!;

        public Supplier Supplier { get; set; } = null!;

        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    }
}