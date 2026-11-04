namespace UniversityCafeteria.Models;
public class SupplyItem
{
    public string ProductName { get; set; } = "";
    public string Category { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Priority { get; set; } = "Normal";
    public decimal LineTotal => Quantity * UnitPrice;
}
public class RequestSummary
{
    public List<SupplyItem> Items { get; set; } = new();
    public string Priority { get; set; } = "Normal";
    public decimal Subtotal => Items.Sum(x => x.LineTotal);
    public decimal PreparationFee => Priority switch { "Urgent" => 5m, "Emergency" => 10m, _ => 2m };
    public decimal DiscountRate => Subtotal > 200m ? .15m : Subtotal > 100m ? .10m : 0m;
    public decimal Discount => decimal.Round(Subtotal * DiscountRate, 2);
    public decimal FinalTotal => Subtotal + PreparationFee - Discount;
}
public class ConfirmedRequest
{
    public int RequestId { get; set; }
    public int ProductCount { get; set; }
    public decimal Subtotal { get; set; }
    public decimal PreparationFee { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalTotal { get; set; }
    public string Status { get; set; } = "Confirmed";
    public DateTime ConfirmedAtUtc { get; set; }
}
public class RequestPageModel
{
    public RequestSummary Summary { get; set; } = new();
    public string PreferredPriority { get; set; } = "Normal";
}
