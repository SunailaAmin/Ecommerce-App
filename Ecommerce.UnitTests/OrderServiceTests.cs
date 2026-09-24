using Ecommerce.Data.Repositories;
using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Ecommerce.Services.Interfaces;
using FluentAssertions;
using Hangfire;
using Moq;
using Ecommerce.Services.Hubs;
using Microsoft.AspNetCore.SignalR;


namespace Ecommerce.UnitTests;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrder_ShouldCreateOrder_WhenProductExists()
    {
        // Arrange
        var orderRepo = new Mock<IOrderRepository>();
        var productRepo = new Mock<IProductRepository>();

        var backgroundJobClient =
            new Mock<IBackgroundJobClient>();

        var hubContext =
            new Mock<IHubContext<NotificationHub>>();

        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 50000,
            Stock = 10
        };

        productRepo
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        orderRepo
            .Setup(x => x.CreateAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order o) => o);

        var service = new OrderService(
        orderRepo.Object,
        productRepo.Object,
        backgroundJobClient.Object,
        hubContext.Object);

        var dto = new CreateOrderDto
        {
            UserId = 1,
            Items = new List<CreateOrderItemDto>
            {
                new CreateOrderItemDto
                {
                    ProductId = 1,
                    Quantity = 2
                }
            }
        };

        // Act
        var result =
            await service.CreateOrderAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.TotalAmount.Should().Be(100000);
        result.OrderItems.Count.Should().Be(1);
    }
}