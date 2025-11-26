using CarWash.Domain.Entities;
using Xunit;

namespace CarWash.Domain.UnitTests;

public class ServiceTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var service = new Service("Мойка", "Полная мойка", 500m);

        Assert.Equal("Мойка", service.Name);
        Assert.Equal("Полная мойка", service.Description);
        Assert.Equal(500m, service.Price);
        Assert.NotEqual(Guid.Empty, service.Id);
    }

    [Fact]
    public void Constructor_Throws_WhenPriceIsNegative()
    {
        Assert.Throws<ArgumentException>(() => new Service("Мойка", "Описание", -100m));
    }
}