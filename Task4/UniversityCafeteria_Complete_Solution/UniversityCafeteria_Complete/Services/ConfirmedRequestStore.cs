using UniversityCafeteria.Models;
namespace UniversityCafeteria.Services;
// Educational in-memory list: resets when the application restarts.
public sealed class ConfirmedRequestStore
{
    private readonly List<ConfirmedRequest> _requests = new();
    private readonly object _gate = new();
    private int _nextId = 1;
    public ConfirmedRequest Add(RequestSummary summary)
    {
        lock (_gate)
        {
            var result = new ConfirmedRequest {
                RequestId = _nextId++, ProductCount = summary.Items.Count,
                Subtotal = summary.Subtotal, PreparationFee = summary.PreparationFee,
                Discount = summary.Discount, FinalTotal = summary.FinalTotal,
                Status = "Confirmed", ConfirmedAtUtc = DateTime.UtcNow
            };
            _requests.Add(result);
            return result;
        }
    }
    public List<ConfirmedRequest> All() { lock (_gate) return _requests.OrderByDescending(r => r.RequestId).ToList(); }
}
