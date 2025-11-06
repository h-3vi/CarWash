namespace CarWash.Application.Models.Services;

public record CreateServiceDto(
    string Name,
    string Description,
    decimal Price
);