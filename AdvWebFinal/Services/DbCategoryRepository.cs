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
    }
}
