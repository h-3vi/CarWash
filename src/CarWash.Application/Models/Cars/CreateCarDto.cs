namespace CarWash.Application.Models.Cars;

public record CreateCarDto(
    string Brand,
    string Model,
    string LicensePlate,
    Guid ClientId
);