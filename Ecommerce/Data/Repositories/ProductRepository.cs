using Ecommerce.Data;
using Ecommerce.Domain;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existingProduct =
            await _context.Products.FindAsync(product.Id);

        if (existingProduct == null)
            return null;

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;

        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product =
            await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task AddRangeAsync(
    List<Product> products)
    {
        await _context.Products.AddRangeAsync(products);

        await _context.SaveChangesAsync();
    }
}