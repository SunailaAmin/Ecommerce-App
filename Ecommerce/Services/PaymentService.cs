using Ecommerce.Data.Repositories;
using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Hangfire;
using Ecommerce.Services.BackgroundJobs;
using Ecommerce.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Ecommerce.Services;

public class PaymentService : IPaymentService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;

    private readonly IHubContext<NotificationHub> _hubContext;

    public PaymentService(
     IOrderRepository orderRepository,
     IPaymentRepository paymentRepository,
     IHubContext<NotificationHub> hubContext)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _hubContext = hubContext;
    }

    public async Task<Payment> ProcessPaymentAsync(
        CreatePaymentDto dto)
    {
        var order =
            await _orderRepository.GetByIdAsync(dto.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.TotalAmount,
            PaymentMethod = dto.PaymentMethod,
            Status = "Success"
        };


        await _paymentRepository.CreateAsync(payment);

        order.Status = "Processing";

        await _orderRepository.UpdateAsync(order);

        BackgroundJob.Enqueue<OrderJobService>(
            x => x.ProcessPayment(order.Id));

        return payment;
    }

    public async Task<Payment?> GetByOrderIdAsync(int orderId)
    {
        return await _paymentRepository.GetByOrderIdAsync(orderId);
    }
}