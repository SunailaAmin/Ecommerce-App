using Ecommerce.Data.Repositories;
using Ecommerce.Domain;
using Ecommerce.Services;
using Ecommerce.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Ecommerce.UnitTests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductById_ShouldReturnProduct()
    {
        // Arrange
        var repoMock =
            new Mock<IProductRepository>();

        repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                Stock = 10
            });

        var memoryCache =
            new MemoryCache(
                new MemoryCacheOptions());

        var service =
            new ProductService(
                repoMock.Object,
                memoryCache);

        // Act
        var result =
            await service.GetProductByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Laptop");
        result.Price.Should().Be(50000);
    }
}