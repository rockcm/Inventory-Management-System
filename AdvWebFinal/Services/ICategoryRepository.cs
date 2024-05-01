using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> ReadAllAsync();

        Task<Category?> ReadAsync(int id);

		Task<Category> UpdateAsync(Category cat);
		Task<Category> CreateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
