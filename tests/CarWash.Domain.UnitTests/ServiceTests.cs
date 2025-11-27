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
    public void Constructor_Throws_WhenNameIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Service(null!, "Описание", 500m));
    }

    [Fact]
    public void Constructor_Throws_WhenNameIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Service("", "Описание", 500m));
    }

    [Fact]
    public void Constructor_Throws_WhenDescriptionIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Service("Мойка", null!, 500m));
    }

    [Fact]
    public void Constructor_Throws_WhenPriceIsNegative()
    {
        Assert.Throws<ArgumentException>(() => new Service("Мойка", "Описание", -100m));
    }

    [Fact]
    public void Constructor_GeneratesUniqueIds()
    {
        var service1 = new Service("Мойка", "Полная", 500m);
        var service2 = new Service("Полировка", "Кузов", 2000m);
        
        Assert.NotEqual(service1.Id, service2.Id);
        Assert.NotEqual(Guid.Empty, service1.Id);
    }

    [Fact]
    public void Constructor_TrimNameAndDescription()
    {
        var service = new Service("  Мойка  ", "  Полная мойка  ", 500m);
        Assert.Equal("Мойка", service.Name);
        Assert.Equal("Полная мойка", service.Description);
    }
}