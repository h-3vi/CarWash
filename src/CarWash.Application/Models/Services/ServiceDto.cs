namespace CarWash.Application.Models.Services;

public record ServiceDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price
);