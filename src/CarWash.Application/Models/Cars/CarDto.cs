namespace CarWash.Application.Models.Cars;

public record CarDto(
    Guid Id,
    string Brand,
    string Model,
    string LicensePlate,
    Guid ClientId
);