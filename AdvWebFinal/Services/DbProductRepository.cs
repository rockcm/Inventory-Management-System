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
            var products = await _db.Products
                .Include(p => p.ProductCategory)
                    .ThenInclude(pc => pc.Category)
                .ToListAsync();

            // Manually flatten the query result
            return products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SellPrice = p.SellPrice,
                PurchasePrice = p.PurchasePrice,
                Stock = p.Stock,
                Image = p.Image,
                ProductCategory = p.ProductCategory.Select(pc => new ProductCategory
                {
                    CategoryId = pc.CategoryId,
                    Category = new Category
                    {
                        
                        Name = pc.Category.Name
                       
                    }
                }).ToList()
            }).ToList();
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
        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _db.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<List<(Category category, List<Product> products)>> GetProductsByCategoryAsync()
        {
            var categoriesWithProducts = await _db.Categories
                .Select(category => new
                {
                    category,
                    products = category.CategoryProduct.Select(pc => pc.Product).ToList()
                })
                .ToListAsync();

            return categoriesWithProducts.Select(cwp => (cwp.category, cwp.products)).ToList();
        }

		public async Task RemoveCategoryFromProductAsync(int productId, int categoryId)
		{
			var product = await _db.Products.FindAsync(productId);
			if (product != null)
			{
				var productCategory = product.ProductCategory.FirstOrDefault(pc => pc.CategoryId == categoryId);
				if (productCategory != null)
				{
					product.ProductCategory.Remove(productCategory);
					await _db.SaveChangesAsync();
				}
			}
		}

	}
}
