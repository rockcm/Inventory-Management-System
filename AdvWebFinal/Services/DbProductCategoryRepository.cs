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
            
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                
                return null;
            }

            var category = await _categoryRepo.ReadAsync(categoryId);
            if (category == null)
            {
           
                return null;
            }
         
            var existingProductCategory = product.ProductCategory.FirstOrDefault(pc => pc.CategoryId == categoryId);
            if (existingProductCategory != null)
            {
              
                return null;
            }
    
            var productCategory = new ProductCategory
            {
                Product = product,
                Category = category
            };
    
            product.ProductCategory.Add(productCategory);
            category.CategoryProduct.Add(productCategory);

          
            await _db.SaveChangesAsync();

            return productCategory;
        }

        public async Task RemoveAsync(int prodId, int CatId)
        {
            var product = await _productRepo.ReadAsync(prodId);

            var prodCat = await _db.ProductCategories
                .Include(pc => pc.Category)
                .FirstOrDefaultAsync(pc => pc.ProductId == prodId && pc.CategoryId == CatId); 

            if (prodCat == null)
            {
                throw new Exception($"ProductCategory with product ID {prodId} and category ID {CatId} not found.");
            }

            var cat = prodCat.Category;

            product.ProductCategory.Remove(prodCat);
            cat.CategoryProduct.Remove(prodCat);

            await _db.SaveChangesAsync();
        }


    }


}
