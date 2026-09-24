using Ecommerce.Domain;

namespace Ecommerce.Data.Repositories;

public interface IPaymentRepository
{
    Task<Payment> CreateAsync(Payment payment);

    Task<Payment?> GetByOrderIdAsync(int orderId);

}