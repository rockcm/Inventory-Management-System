using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> ReadAllAsync();

        Task<Category?> ReadAsync(int id);

    }
}
