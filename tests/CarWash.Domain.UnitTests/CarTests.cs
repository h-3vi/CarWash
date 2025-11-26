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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Throws_WhenBrandIsInvalid(string? brand) 
    {
        Assert.Throws<ArgumentException>(() => new Car(brand!, "Camry", "А123БВ777", Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_Throws_WhenClientIdIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Car("Toyota", "Camry", "А123БВ777", Guid.Empty));
    }
}