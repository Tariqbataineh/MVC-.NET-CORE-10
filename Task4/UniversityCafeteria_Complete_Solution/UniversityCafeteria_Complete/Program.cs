using UniversityCafeteria.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
builder.Services.AddSingleton<ConfirmedRequestStore>();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/SupplyRequest/Error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=SupplyRequest}/{action=Index}/{id?}");
app.Run();
