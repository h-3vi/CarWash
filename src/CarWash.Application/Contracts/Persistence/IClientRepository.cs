using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Persistence;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client> AddAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task DeleteAsync(Guid id);
}