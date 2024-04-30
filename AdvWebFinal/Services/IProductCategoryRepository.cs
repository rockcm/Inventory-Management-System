using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface IProductCategoryRepository
    {

        Task<ICollection<ProductCategory>> ReadAllAsync();

        Task<ProductCategory?> ReadAsync(int id);
        Task<ProductCategory?> CreateAsync(int productId, int categoryId);
		Task RemoveAsync(int prodId, int catId);
	}
}
