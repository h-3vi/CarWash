using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Persistence;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetByStatusAsync(OrderStatus status);
    Task<List<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order> AddAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}