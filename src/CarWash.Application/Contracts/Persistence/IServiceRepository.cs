using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Persistence;

public interface IServiceRepository
{
    Task<List<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(Guid id);
    Task<Service> AddAsync(Service service);
    Task<Service> UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}