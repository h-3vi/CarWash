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
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Марка не может быть пустой", nameof(brand));
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Модель не может быть пустой", nameof(model));
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("Госномер не может быть пустым", nameof(licensePlate));
        if (clientId == Guid.Empty)
            throw new ArgumentException("ID клиента не может быть пустым", nameof(clientId));

        Id = Guid.NewGuid();
        Brand = brand.Trim();
        Model = model.Trim();
        LicensePlate = licensePlate.Trim();
        ClientId = clientId;
    }
}