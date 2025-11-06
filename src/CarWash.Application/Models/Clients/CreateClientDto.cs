namespace CarWash.Application.Models.Clients;

public record CreateClientDto(
    string FullName,
    string PhoneNumber
);