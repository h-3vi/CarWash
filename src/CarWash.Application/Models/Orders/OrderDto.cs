using CarWash.Domain.Entities;

namespace CarWash.Application.Models.Orders;

public record OrderDto(
    Guid Id,
    Guid ClientId,
    Guid CarId,
    List<Guid> ServiceIds,
    DateTime OrderDate,
    decimal TotalAmount,
    OrderStatus Status
);