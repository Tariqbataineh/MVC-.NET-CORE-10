using AcademyGraduationManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyGraduationManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Offer> Offers { get; set; }
    }
}