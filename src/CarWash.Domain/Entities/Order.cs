namespace CarWash.Domain.Entities;

public enum OrderStatus
{
    Pending,      
    InProgress,   
    Completed     
}

public class Order
{
    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid CarId { get; private set; }

    private readonly List<Guid> _serviceIds = new();
    public IReadOnlyCollection<Guid> ServiceIds => _serviceIds.AsReadOnly();

    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public Client? Client { get; private set; }
    public Car? Car { get; private set; }

    private Order() { } 

    public Order(Guid clientId, Guid carId, DateTime orderDate, OrderStatus status = OrderStatus.Pending)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        CarId = carId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = 0;
    }

    public void AddService(Guid serviceId)
    {
        if (!_serviceIds.Contains(serviceId))
            _serviceIds.Add(serviceId);
    }

    public void SetTotalAmount(decimal amount)
    {
        TotalAmount = amount;
    }

    public void SetStatus(OrderStatus status)
    {
        Status = status;
    }
}