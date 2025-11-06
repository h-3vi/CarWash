namespace CarWash.Domain.Entities;

public class Car
{
    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public string LicensePlate { get; private set; } = string.Empty;

    public Guid ClientId { get; private set; }
    public Client? Client { get; private set; }

    private Car() { } 

    public Car(string brand, string model, string licensePlate, Guid clientId)
    {
        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        LicensePlate = licensePlate;
        ClientId = clientId;
    }
}