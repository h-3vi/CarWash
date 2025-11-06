using CarWash.Application.Contracts.Persistence;
using CarWash.Infrastructure.Persistence;
using CarWash.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarWash.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CarWashDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}