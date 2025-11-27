using CarWash.Domain.Entities;
using Xunit;

namespace CarWash.Domain.UnitTests;

public class CarTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var clientId = Guid.NewGuid();
        var car = new Car("Toyota", "Camry", "А123БВ777", clientId);

        Assert.Equal("Toyota", car.Brand);
        Assert.Equal("Camry", car.Model);
        Assert.Equal("А123БВ777", car.LicensePlate);
        Assert.Equal(clientId, car.ClientId);
        Assert.NotEqual(Guid.Empty, car.Id);
    }

    [Fact]
    public void Constructor_Throws_WhenBrandIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Car(null!, "Camry", "А123БВ777", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenBrandIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Car("", "Camry", "А123БВ777", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenModelIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", null!, "А123БВ777", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenModelIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", "", "А123БВ777", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenLicensePlateIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", "Camry", null!, Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenLicensePlateIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", "Camry", "", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenClientIdIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", "Camry", "А123БВ777", Guid.Empty));
    }
}