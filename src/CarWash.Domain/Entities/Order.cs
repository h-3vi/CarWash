using System.Collections.ObjectModel;

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
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public Client? Client { get; private set; }
    public Car? Car { get; private set; }

    private readonly List<OrderService> _orderServices = new();
    public IReadOnlyCollection<OrderService> OrderServices => _orderServices.AsReadOnly();

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
        if (!_orderServices.Any(os => os.ServiceId == serviceId))
        {
            _orderServices.Add(new OrderService { OrderId = Id, ServiceId = serviceId });
        }
    }

    public void SetTotalAmount(decimal amount) => TotalAmount = amount;
    public void SetStatus(OrderStatus status) => Status = status;
}