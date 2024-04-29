using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvWebFinal.Services
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> ReadAllAsync();
     
        Task<Product?> ReadAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);

        Task DeleteAsync(Product prod);

        Task<List<Product>> SearchProductsAsync(string searchTerm);

        Task<List<(Category category, List<Product> products)>> GetProductsByCategoryAsync();


    }
}
