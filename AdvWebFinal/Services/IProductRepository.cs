using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> ReadAllAsync();
        Task<ICollection<Product>> ReadAllAsync2();
        Task<Product?> ReadAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
    }
}
