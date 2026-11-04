# University Cafeteria — Complete MVC Solution

Includes: original Cafeteria (Task 2), Supply (Day 3), and new Supply Request Management feature (uploaded 6-page task).

## Run
1. Install .NET 10 SDK and open `UniversityCafeteria.csproj` in Visual Studio 2026 (or terminal).
2. Run `dotnet restore` then `dotnet run` from this folder, or use Visual Studio Run (HTTPS profile if available).
3. Open the local HTTPS address printed by the app. Default page: `/SupplyRequest/Index`.

## Supply Request workflow
- Add multiple items on `/SupplyRequest/Index` (POST Model Binding).
- Review cost breakdown on `/SupplyRequest/Review` (per-line cost, subtotal, priority fee, discount and final total).
- Confirm or clear draft (POST). Draft items are serialized as JSON in ASP.NET Session.
- Save a preferred priority for 7 days using an HTTPS-only `PreferredPriority` browser Cookie.
- Confirmed requests appear in `/SupplyRequest/MyRequests` and are held in a thread-safe in-memory List.

### Calculation rules
- Each line: Quantity × UnitPrice.
- Subtotal: sum of lines.
- Preparation fee (priority of most recently added item): Normal=2 JD, Urgent=5 JD, Emergency=10 JD.
- Discount on subtotal only: >200 JD=15%, else >100 JD=10%, otherwise 0%.
- Final Total = Subtotal + Preparation Fee − Discount.

## Important educational limitations
- Confirmed requests live only in application memory: application restart clears them; this demo has no user login or database, so My Requests shows every request in the running app.
- The specification allows multiple item priorities but only a single preparation fee. This solution uses the **most recently added item's priority** as the request-level priority; explicitly documented to avoid ambiguity.
- `Secure = true` on the cookie requires HTTPS. Use the `https://` URL in development.
- Data is validated in the Controller; this is an educational project, not a production ordering service.
- CDN Bootstrap requires internet connectivity for styling.

## Verification
Source file/layout/action checks were performed. A .NET SDK was not installed in the packaging environment, so `dotnet build` and browser interaction tests could not be executed here. Run `dotnet build` locally to verify compilation.
