using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);

        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetByUserIdAsync(
    int userId)
    {
        return await _context.Orders
            .Where(x => x.UserId == userId)
            .Include(x => x.OrderItems)
            .ToListAsync();
    }
}