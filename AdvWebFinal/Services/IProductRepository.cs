using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> ReadAllAsync();
        Task<Product?> ReadAsync(int id);
    }
}
