using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvWebFinal.Services
{
    public class DbProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;

        public DbProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ICollection<Product>> ReadAllAsync()
        {
            
            return await _db.Products
            .Include(p => p.ProductCategory)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();
        }

        public async Task<Product?> ReadAsync(int id)
        {

            return await _db.Products
            .Include(p => p.ProductCategory)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }


    }
}
