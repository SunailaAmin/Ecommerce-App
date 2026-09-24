using Ecommerce.Data.Repositories;
using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Hangfire;
using Ecommerce.Services.BackgroundJobs;
using Ecommerce.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Ecommerce.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IBackgroundJobClient _backgroundJobClient;

    private readonly IHubContext<NotificationHub> _hubContext;

    public OrderService(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IBackgroundJobClient backgroundJobClient,
    IHubContext<NotificationHub> hubContext)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _backgroundJobClient = backgroundJobClient;
        _hubContext = hubContext;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            UserId = dto.UserId,
            Status = "Pending"
        };

        decimal totalAmount = 0;

        foreach (var item in dto.Items)
        {
            var product =
                await _productRepository.GetByIdAsync(item.ProductId);

            if (product == null)
                throw new Exception($"Product {item.ProductId} not found");

            if (product.Stock < item.Quantity)
                throw new Exception($"Insufficient stock for {product.Name}");

            product.Stock -= item.Quantity;

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                Price = product.Price
            };

            order.OrderItems.Add(orderItem);

            totalAmount +=
                product.Price * item.Quantity;

            await _productRepository.UpdateAsync(product);
        }

        order.TotalAmount = totalAmount;

        var createdOrder =
        await _orderRepository.CreateAsync(order);

        await _hubContext.Clients.All.SendAsync(
            "ReceiveOrderNotification",
            $"New Order Created: {createdOrder.Id}");

        _backgroundJobClient.Enqueue<OrderJobService>(
            x => x.SendOrderConfirmation(createdOrder.Id));
        return createdOrder;
    }   

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }
    public async Task<Order?> UpdateStatusAsync(
    int id,
    string status)
    {
        var order =
            await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return null;

        order.Status = status;

        await _orderRepository.UpdateAsync(order);

        return order;
    }

    public async Task<bool> CancelOrderAsync(int id)
    {
        var order =
            await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return false;

        if (order.Status == "Shipped")
            return false;

        order.Status = "Cancelled";

        await _orderRepository.UpdateAsync(order);

        return true;
    }

    public async Task<List<Order>> GetUserOrdersAsync(
    int userId)
    {
        return await _orderRepository
            .GetByUserIdAsync(userId);
    }
}