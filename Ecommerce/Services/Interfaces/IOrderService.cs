using Ecommerce.Domain;
using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(CreateOrderDto dto);

    Task<Order?> GetOrderByIdAsync(int id);

    Task<List<Order>> GetAllOrdersAsync();

    Task<Order?> UpdateStatusAsync(int id,string status);

    Task<bool> CancelOrderAsync(int id);

    Task<List<Order>> GetUserOrdersAsync( int userId);
}