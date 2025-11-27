using CarWash.Domain.Entities;
using Xunit;

namespace CarWash.Domain.UnitTests;

public class OrderTests
{
    [Fact]
    public void Constructor_SetsInitialStateCorrectly()
    {
        var clientId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var order = new Order(clientId, carId, now);

        Assert.Equal(clientId, order.ClientId);
        Assert.Equal(carId, order.CarId);
        Assert.Equal(now.Date, order.OrderDate.Date);
        Assert.Equal(0m, order.TotalAmount);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Empty(order.OrderServices);
    }

    [Fact]
    public void AddService_AddsUniqueServiceOnly()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        var service1 = Guid.NewGuid();
        var service2 = Guid.NewGuid();

        order.AddService(service1);
        order.AddService(service1); 
        order.AddService(service2);

        var services = order.GetServiceIds().ToList();
        Assert.Equal(2, services.Count);
        Assert.Contains(service1, services);
        Assert.Contains(service2, services);
    }

    [Fact]
    public void SetTotalAmount_SetsCorrectValue()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        order.SetTotalAmount(1234.56m);
        Assert.Equal(1234.56m, order.TotalAmount);
    }

    [Fact]
    public void SetStatus_ChangesOrderStatus()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        order.SetStatus(OrderStatus.Completed);
        Assert.Equal(OrderStatus.Completed, order.Status);
    }

    [Fact]
    public void AddService_Throws_WhenServiceIdIsEmpty()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        Assert.Throws<ArgumentException>(() => order.AddService(Guid.Empty));
    }

    [Fact]
    public void SetTotalAmount_Throws_WhenAmountIsNegative()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        Assert.Throws<ArgumentException>(() => order.SetTotalAmount(-100m));
    }
}