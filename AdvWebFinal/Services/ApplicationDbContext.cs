using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvWebFinal.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.SellPrice)
                .HasColumnType("decimal(18, 2)"); // Specify precision and scale according to your needs

            modelBuilder.Entity<Product>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18, 2)"); // Specify precision and scale according to your needs
        }

      

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        
    }
}
