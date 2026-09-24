using Ecommerce.Domain;
using Ecommerce.Services.Interfaces;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock
        };

        var createdProduct =
            await _productService.CreateProductAsync(product);

        return Ok(createdProduct);
    
    }

    [ResponseCache(Duration = 60)]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        int id,
        UpdateProductDto dto)
    {
        var existingProduct =
            await _productService.GetProductByIdAsync(id);

        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = dto.Name;
        existingProduct.Description = dto.Description;
        existingProduct.Price = dto.Price;
        existingProduct.Stock = dto.Stock;

        var updatedProduct =
            await _productService.UpdateProductAsync(existingProduct);

        return Ok(updatedProduct);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result =
            await _productService.DeleteProductAsync(id);

        if (!result)
            return NotFound();

        return Ok("Product deleted successfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/stock")]
    public async Task<IActionResult> UpdateStock(
    int id,
    UpdateStockDto dto)
    {
        var product =
            await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound();

        product.Stock = dto.Stock;

        await _productService.UpdateProductAsync(product);

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("batch")]
    public async Task<IActionResult> BatchUpload(
    List<CreateProductDto> products)
    {
        var count =
            await _productService
                .BatchCreateAsync(products);

        return Ok(new
        {
            Message =
                $"{count} products uploaded successfully"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("bulk-upload")]
    public async Task<IActionResult> BulkUpload(
    IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required");

        var count =
            await _productService
                .BulkUploadAsync(file);

        return Ok(new
        {
            Message =
                $"{count} products uploaded successfully"
        });
    }
}