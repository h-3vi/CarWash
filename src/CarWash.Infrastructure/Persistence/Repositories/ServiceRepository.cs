using CarWash.Application.Contracts.Persistence;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.Persistence.Repositories;

public class ServiceRepository : BaseRepository<Service>, IServiceRepository
{
    public ServiceRepository(CarWashDbContext context) : base(context) { }
}