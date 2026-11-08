using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaManagement.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CafeteriaDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CafeteriaConnection"));
});

builder.Services.AddDbContext<CodeFirstDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CodeFirstConnection"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();