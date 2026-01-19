using Microsoft.EntityFrameworkCore;
using MyVinCafe.Models;

namespace MyVinCafe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Tabel User kita
    }
}