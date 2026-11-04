using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Models;
using UniversityCafeteria.Services;
namespace UniversityCafeteria.Controllers;
public class SupplyRequestController : Controller
{
    private const string DraftKey = "SupplyRequestDraft";
    private const string PriorityKey = "SupplyRequestPriority";
    private const string PreferenceCookie = "PreferredPriority";
    private static readonly string[] ValidPriorities = { "Normal", "Urgent", "Emergency" };
    private readonly ConfirmedRequestStore _store;
    public SupplyRequestController(ConfirmedRequestStore store) => _store = store;

    private List<SupplyItem> GetDraft()
    {
        var json = HttpContext.Session.GetString(DraftKey);
        if (string.IsNullOrEmpty(json)) return new List<SupplyItem>();
        try { return JsonSerializer.Deserialize<List<SupplyItem>>(json) ?? new List<SupplyItem>(); }
        catch (JsonException) { return new List<SupplyItem>(); }
    }
    private void SaveDraft(List<SupplyItem> items) => HttpContext.Session.SetString(DraftKey, JsonSerializer.Serialize(items));
    private string PreferredPriority()
    {
        var cookie = Request.Cookies[PreferenceCookie];
        return ValidPriorities.Contains(cookie) ? cookie! : "Normal";
    }
    private RequestSummary Summary() => new() {
        Items = GetDraft(),
        Priority = ValidPriorities.Contains(HttpContext.Session.GetString(PriorityKey))
            ? HttpContext.Session.GetString(PriorityKey)! : PreferredPriority()
    };
    [HttpGet]
    public IActionResult Index() => View(new RequestPageModel { Summary = Summary(), PreferredPriority = PreferredPriority() });

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddItem(string productName, string category, int quantity, decimal unitPrice, string priority)
    {
        productName = productName?.Trim() ?? "";
        category = category?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(productName) || quantity <= 0 || unitPrice <= 0 || !ValidPriorities.Contains(priority))
        {
            TempData["Error"] = "Enter a product name, positive quantity and unit price, and a valid priority.";
            return RedirectToAction(nameof(Index));
        }
        var draft = GetDraft();
        draft.Add(new SupplyItem { ProductName = productName, Category = category, Quantity = quantity, UnitPrice = unitPrice, Priority = priority });
        SaveDraft(draft);
        // Request-level priority follows the most recently added item.
        HttpContext.Session.SetString(PriorityKey, priority);
        TempData["Message"] = $"{productName} added to your draft.";
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Review() => View(Summary());

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ConfirmRequest()
    {
        var summary = Summary();
        if (summary.Items.Count == 0)
        {
            TempData["Error"] = "You cannot confirm an empty supply request.";
            return RedirectToAction(nameof(Index));
        }
        var saved = _store.Add(summary);
        HttpContext.Session.Remove(DraftKey);
        HttpContext.Session.Remove(PriorityKey);
        TempData["Message"] = $"Request #{saved.RequestId} confirmed successfully.";
        return RedirectToAction(nameof(MyRequests));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ClearDraft()
    {
        HttpContext.Session.Remove(DraftKey);
        HttpContext.Session.Remove(PriorityKey);
        TempData["Message"] = "Draft cleared.";
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SavePreference(string priority)
    {
        if (!ValidPriorities.Contains(priority))
        {
            TempData["Error"] = "Choose a valid priority.";
            return RedirectToAction(nameof(Index));
        }
        Response.Cookies.Append(PreferenceCookie, priority, new CookieOptions {
            Expires = DateTimeOffset.UtcNow.AddDays(7), HttpOnly = true, Secure = true,
            SameSite = SameSiteMode.Lax, IsEssential = true
        });
        TempData["Message"] = $"Your preferred request priority is {priority}.";
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult MyRequests() => View(_store.All());
    [HttpGet]
    public IActionResult About() => View();
    [HttpGet]
    public IActionResult Error() => View();
}
