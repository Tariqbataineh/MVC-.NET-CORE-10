# Orange University Cafeteria — ASP.NET Core MVC Day 3

A complete **.NET 10 MVC** classroom project implementing the supplied **University Cafeteria Day 3 Task**. Includes six Razor Views, `SupplyController`, a polished shared layout, Bootstrap cards, and a custom responsive stylesheet.

## Run
1. Open `UniversityCafeteria.csproj` in Visual Studio (with .NET 10 SDK installed).
2. Run the **https** launch profile (F5 or Ctrl+F5).
3. Open `/Supply/Index` (the default route already points here).

This project uses a Bootstrap CDN and optional Google Fonts CDN. A network connection is required for those external styles; its own CSS is local.

## Requirements checklist
- `SupplyController`: Index, Products, Suppliers, CreateRequest (GET), SubmitRequest (POST), About.
- `ViewData`: Products page title, manager Ahmad Ali, count 5, products string array, Bootstrap metric cards.
- `ViewBag`: Suppliers page title, count 3, supplier names.
- `ViewBag.Categories` + looping through categories in Index.
- Form: EmployeeName (text), ProductName (select), Quantity (number), RequiredDate (date).
- `Request.Form`: read all four posted values; validate missing employee name and other malformed inputs.
- `TempData["Message"]` success message; `RedirectToAction("About")`; About displays it.
- Shared Layout: Home, Products, Suppliers, Create Request, About navigation.
- Optional `CheckQuantity(int quantity)` bonus: direct response to `/Supply/CheckQuantity?quantity=25` returns `Medium Order`.

## Test the form
- Open `/Supply/CreateRequest`.
- Leave Employee Name empty, fill other inputs, submit -> `/Supply/SubmitRequest` shows an error.
- Fill valid values -> redirect to `/Supply/About` with a green success notification.

No database is used. A successful request displays confirmation **without saving a record**.
