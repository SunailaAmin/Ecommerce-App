using Ecommerce.Domain;
using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();

    Task<Product?> GetProductByIdAsync(int id);

    Task<Product> CreateProductAsync(Product product);

    Task<Product?> UpdateProductAsync(Product product);

    Task<bool> DeleteProductAsync(int id);

    Task<int> BatchCreateAsync(List<CreateProductDto> products);

    Task<int> BulkUploadAsync(IFormFile file);
}