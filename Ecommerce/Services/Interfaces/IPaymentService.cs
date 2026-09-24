using Ecommerce.Domain;
using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(CreatePaymentDto dto);

    Task<Payment?> GetByOrderIdAsync(int orderId);
}