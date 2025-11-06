using CarWash.Application.Contracts.Persistence;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Persistence.Repositories;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    public CarRepository(CarWashDbContext context) : base(context) { }

    public async Task<List<Car>> GetByClientIdAsync(Guid clientId)
    {
        return await _context.Cars
            .Where(c => c.ClientId == clientId)
            .ToListAsync();
    }
}