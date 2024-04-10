using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvWebFinal.Services
{
    public class DbProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;

        public DbProductCategoryRepository(ApplicationDbContext db, IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _db = db;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public async Task<ProductCategory?> ReadAsync(int id)
        {
            return await _db.ProductCategories
               .Include(pc => pc.Product)
               .Include(pc => pc.Category)
               .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ICollection<ProductCategory>> ReadAllAsync()
        {
            return await _db.ProductCategories 
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
               .ToListAsync();
        }

        public async Task<ProductCategory> CreateAsync(int productId, int categoryId)
        {
            // Retrieve the product and category
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                // The product was not found
                return null;
            }

            var category = await _categoryRepo.ReadAsync(categoryId);
            if (category == null)
            {
                // The category was not found
                return null;
            }

            // Check if the product already has the category assigned
            var existingProductCategory = product.ProductCategory.FirstOrDefault(pc => pc.CategoryId == categoryId);
            if (existingProductCategory != null)
            {
                // The product already has the category assigned
                return null;
            }

            // Create a new ProductCategory instance
            var productCategory = new ProductCategory
            {
                Product = product,
                Category = category
            };

            // Add the product category to the product and category collections
            product.ProductCategory.Add(productCategory);
            category.CategoryProduct.Add(productCategory);

            // Save changes to the database
            await _db.SaveChangesAsync();

            return productCategory;
        }

    }
}
