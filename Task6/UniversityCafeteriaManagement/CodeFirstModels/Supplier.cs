
namespace UniversityCafeteriaManagement.CodeFirstModels
{
    public class Supplier
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string? Phone { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public List<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}