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

        public async Task<ICollection<Product>> ReadAllAsync2()
        {

            return await _db.Products
           
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

        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = await ReadAsync(product.Id);

            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {product.Id} not found.");
            }

            // Update properties of the existing product
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.SellPrice = product.SellPrice;
            existingProduct.PurchasePrice = product.PurchasePrice;
            existingProduct.Stock = product.Stock;
            existingProduct.Image = product.Image;

            // Save changes to the database
            await _db.SaveChangesAsync();

            return existingProduct;
        }

    }
}
