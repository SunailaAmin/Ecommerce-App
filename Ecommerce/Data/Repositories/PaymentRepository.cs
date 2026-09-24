using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> CreateAsync(Payment payment)
    {
        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<Payment?> GetByOrderIdAsync(int orderId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}