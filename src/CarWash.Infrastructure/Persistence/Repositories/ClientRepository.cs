using CarWash.Application.Contracts.Persistence;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.Persistence.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(CarWashDbContext context) : base(context) { }
}