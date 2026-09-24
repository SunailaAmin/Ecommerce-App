using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(
        IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(
        CreatePaymentDto dto)
    {
        var payment =
            await _paymentService.ProcessPaymentAsync(dto);

        return Ok(new
        {
            payment.Id,
            payment.OrderId,
            payment.Amount,
            payment.PaymentMethod,
            payment.Status
        });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetPayment(int orderId)
    {
        var payment =
            await _paymentService.GetByOrderIdAsync(orderId);

        if (payment == null)
            return NotFound();

        return Ok(payment);
    
    }
}