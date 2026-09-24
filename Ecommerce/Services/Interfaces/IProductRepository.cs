using Ecommerce.Domain;

namespace Ecommerce.Services.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<Product> AddAsync(Product product);

    Task<Product?> UpdateAsync(Product product);

    Task AddRangeAsync(List<Product> products);

    Task<bool> DeleteAsync(int id);
}