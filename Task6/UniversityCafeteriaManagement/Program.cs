using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaManagement.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Part 1 - Database First
builder.Services.AddDbContext<CafeteriaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CafeteriaConnection"));
});

// Part 2 - Code First
builder.Services.AddDbContext<CodeFirstDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeFirstConnection"));
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
