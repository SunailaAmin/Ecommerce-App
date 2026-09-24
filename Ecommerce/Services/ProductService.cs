using Ecommerce.Data.Repositories;
using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml;

namespace Ecommerce.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public ProductService(
    IProductRepository productRepository,
    IMemoryCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        const string cacheKey = "all_products";

        if (_cache.TryGetValue(cacheKey, out List<Product>? products))
        {
            return products!;
        }

        products = await _productRepository.GetAllAsync();

        _cache.Set(
            cacheKey,
            products,
            TimeSpan.FromMinutes(5));

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _cache.Remove("all_products");
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new Exception("Product name is required");

        if (product.Price <= 0)
            throw new Exception("Price must be greater than 0");

        if (product.Stock < 0)
            throw new Exception("Stock cannot be negative");

        return await _productRepository.AddAsync(product);

    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        _cache.Remove("all_products");
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new Exception("Product name is required");

        if (product.Price <= 0)
            throw new Exception("Price must be greater than 0");

        if (product.Stock < 0)
            throw new Exception("Stock cannot be negative");

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        _cache.Remove("all_products");
        return await _productRepository.DeleteAsync(id);
    }

    public async Task<int> BatchCreateAsync(
    List<CreateProductDto> productDtos)
    {
        var products =
            productDtos.Select(dto => new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            })
            .ToList();

        await _productRepository
            .AddRangeAsync(products);

        return products.Count;
    }

    public async Task<int> BulkUploadAsync(
    IFormFile file)
    {
        ExcelPackage.License.SetNonCommercialOrganization("Learning");

        var products =
            new List<CreateProductDto>();

        using var stream =
            new MemoryStream();

        await file.CopyToAsync(stream);

        using var package =
            new ExcelPackage(stream);

        var worksheet =
            package.Workbook.Worksheets[0];

        int rowCount =
            worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            products.Add(new CreateProductDto
            {
                Name =
                    worksheet.Cells[row, 1].Text,

                Description =
                    worksheet.Cells[row, 2].Text,

                Price =
                    decimal.Parse(
                        worksheet.Cells[row, 3].Text),

                Stock =
                    int.Parse(
                        worksheet.Cells[row, 4].Text)
            });
        }

        return await BatchCreateAsync(products);
    }
}