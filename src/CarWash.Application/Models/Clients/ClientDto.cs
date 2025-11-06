namespace CarWash.Application.Models.Clients;

public record ClientDto(
    Guid Id,
    string FullName,
    string PhoneNumber
);