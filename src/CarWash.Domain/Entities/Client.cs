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
        Id = Guid.NewGuid();
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }
}