namespace CarWash.Application.Models.Orders;

public record CreateOrderDto(
    Guid ClientId,
    Guid CarId,
    List<Guid> ServiceIds
);
