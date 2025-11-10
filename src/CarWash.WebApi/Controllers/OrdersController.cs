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
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        IOrderRepository orderRepository,
        IServiceRepository serviceRepository,
        ILogger<OrdersController> logger)
    {
        _orderRepository = orderRepository;
        _serviceRepository = serviceRepository;
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

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByDateRange(
        [FromQuery] DateTime? from, 
        [FromQuery] DateTime? to)
    {
        if (!from.HasValue || !to.HasValue)
            return BadRequest("Параметры 'from' и 'to' обязательны");

        var orders = await _orderRepository.GetByDateRangeAsync(from.Value, to.Value);
        return Ok(orders.Select(MapToDto));
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto dto)
    {
        // Получаем услуги для расчёта суммы
        var services = await _serviceRepository.GetAllAsync();
        var selectedServices = services.Where(s => dto.ServiceIds.Contains(s.Id)).ToList();

        if (selectedServices.Count != dto.ServiceIds.Count)
            return BadRequest("Одна или несколько услуг не найдены");

        var totalAmount = selectedServices.Sum(s => s.Price);

        var order = new Order(dto.ClientId, dto.CarId, DateTime.UtcNow);
        foreach (var svcId in dto.ServiceIds)
            order.AddService(svcId);
        order.SetTotalAmount(totalAmount);

        await _orderRepository.AddAsync(order);
        _logger.LogInformation("Создан заказ ID: {OrderId} на сумму {Amount}", order.Id, totalAmount);

        return CreatedAtAction(nameof(GetOrders), MapToDto(order));
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, UpdateOrderStatusDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return NotFound();

        order.SetStatus(dto.Status);
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Обновлён статус заказа {OrderId} на {Status}", id, dto.Status);
        return NoContent();
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