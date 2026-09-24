using Ecommerce.Domain;

namespace Ecommerce.Data.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);

    Task<Order?> GetByIdAsync(int id);

    Task<List<Order>> GetAllAsync();

    Task<Order> UpdateAsync(Order order);

    Task<List<Order>> GetByUserIdAsync(int userId);


}