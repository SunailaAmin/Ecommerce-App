using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var order =
            await _orderService.CreateOrderAsync(dto);

        return Ok(new
        {
            order.Id,
            order.UserId,
            order.TotalAmount,
            order.Status
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders =
            await _orderService.GetAllOrdersAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order =
            await _orderService.GetOrderByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(new
        {
            order.Id,
            order.UserId,
            order.TotalAmount,
            order.Status,
            order.CreatedAt
        });
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> Checkout(int id)
    {
        var order =
            await _orderService.GetOrderByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(new
        {
            OrderId = order.Id,
            Amount = order.TotalAmount,
            Status = order.Status
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
    int id,
    UpdateOrderStatusDto dto)
    {
        var order =
            await _orderService.UpdateStatusAsync(
                id,
                dto.Status);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var result =
            await _orderService.CancelOrderAsync(id);

        if (!result)
            return BadRequest();

        return Ok("Order cancelled");
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> MyOrders()
    {
        var userId =
            int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        var orders =
            await _orderService
                .GetUserOrdersAsync(userId);

        return Ok(orders);
    }
}