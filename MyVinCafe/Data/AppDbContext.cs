using Microsoft.EntityFrameworkCore;
using MyVinCafe.Models;

namespace MyVinCafe.Data
{
    //defultnya gini lah
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Tabel User kita

        // kalo mau buat admin tapi males buat manual di database/SSsms
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FullName = "Admin Vinku",
                Username = "Admin-Vinku",
                Password = "123",
                Role = "Admin"
            });
        }
    }
}