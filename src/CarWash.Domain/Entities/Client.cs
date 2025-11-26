using System.Collections.ObjectModel;

namespace CarWash.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private Client() { }

    public Client(string fullName, string phoneNumber)
    {
        if (fullName == null)
            throw new ArgumentNullException(nameof(fullName), "ФИО клиента не может быть null");
        if (phoneNumber == null)
            throw new ArgumentNullException(nameof(phoneNumber), "Телефон клиента не может быть null");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ФИО клиента не может быть пустым", nameof(fullName));
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Телефон клиента не может быть пустым", nameof(phoneNumber));

        Id = Guid.NewGuid();
        FullName = fullName.Trim();
        PhoneNumber = phoneNumber.Trim();
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }
}