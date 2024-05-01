using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvWebFinal.Services
{
    public class DbCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public DbCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<Category?> ReadAsync(int id)
        {
            return await _db.Categories
                         .Include(c => c.CategoryProduct)
                         .ThenInclude(pc => pc.Product)
                         .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Category>> ReadAllAsync()
        {

            return await _db.Categories
                        .Include(c => c.CategoryProduct)
                        .ThenInclude(pc => pc.Product)
                        .ToListAsync();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

		public async Task<Category> UpdateAsync(Category cat)
		{
			var existingCat = await ReadAsync(cat.Id);

			if (existingCat == null)
			{
				throw new ArgumentException($"Product with ID {cat.Id} not found.");
			}

			// Update properties of the existing product
			existingCat.Name = cat.Name;
			

			// Save changes to the database
			await _db.SaveChangesAsync();

			return existingCat;
		}

	}
}
