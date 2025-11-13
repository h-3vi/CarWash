using CarWash.Application.Contracts.Persistence;
using CarWash.Application.Models.Orders;
using CarWash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        IOrderRepository orderRepository,
        ILogger<OrdersController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await _orderRepository.GetAllAsync();
        var dtos = orders.Select(MapToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByStatus(string status)
    {
        if (!Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
            return BadRequest("Неверный статус заказа");

        var orders = await _orderRepository.GetByStatusAsync(orderStatus);
        return Ok(orders.Select(MapToDto));
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
    {
        var orders = await _orderRepository.GetByDateRangeAsync(startDate, endDate);
        return Ok(orders.Select(MapToDto));
    }

    private static OrderDto MapToDto(Order order) =>
        new(
            order.Id,
            order.ClientId,
            order.CarId,
            order.OrderServices.Select(os => os.ServiceId).ToList(),
            order.OrderDate,
            order.TotalAmount,
            order.Status
        );
}