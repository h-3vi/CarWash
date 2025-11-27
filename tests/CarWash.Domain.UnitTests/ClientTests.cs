using CarWash.Domain.Entities;
using Xunit;

namespace CarWash.Domain.UnitTests;

public class ClientTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var client = new Client("Иванов Иван", "+79991234567");
        Assert.Equal("Иванов Иван", client.FullName);
        Assert.Equal("+79991234567", client.PhoneNumber);
        Assert.NotEqual(Guid.Empty, client.Id);
    }

    [Fact]
    public void Constructor_Throws_WhenFullNameIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Client(null!, "+79991234567"));
    }

    [Fact]
    public void Constructor_Throws_WhenFullNameIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Client("", "+79991234567"));
    }

    [Fact]
    public void Constructor_Throws_WhenPhoneNumberIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Client("Иванов Иван", null!));
    }

    [Fact]
    public void Constructor_Throws_WhenPhoneNumberIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Client("Иванов Иван", ""));
    }

    [Fact]
    public void AddOrder_AddsOrderToCollection()
    {
        var client = new Client("Иванов Иван", "+79991234567");
        var clientId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var order = new Order(clientId, carId, DateTime.UtcNow);
        client.AddOrder(order);

        Assert.Single(client.Orders);
        Assert.Contains(order, client.Orders);
    }

    [Fact]
    public void Constructor_GeneratesUniqueIds()
    {
        var client1 = new Client("Иванов", "+79991234567");
        var client2 = new Client("Петров", "+79997654321");
        Assert.NotEqual(client1.Id, client2.Id);
        Assert.NotEqual(Guid.Empty, client1.Id);
    }
}