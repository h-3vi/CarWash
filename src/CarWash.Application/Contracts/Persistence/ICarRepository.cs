using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Persistence;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(Guid id);
    Task<List<Car>> GetByClientIdAsync(Guid clientId);
    Task<Car> AddAsync(Car car);
    Task<Car> UpdateAsync(Car car);
    Task DeleteAsync(Guid id);
}