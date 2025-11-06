using CarWash.Domain.Entities;

namespace CarWash.Application.Models.Orders;

public record UpdateOrderStatusDto(
    OrderStatus Status
);