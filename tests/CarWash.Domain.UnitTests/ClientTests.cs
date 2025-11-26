using CarWash.Domain.Entities;
using Xunit;

namespace CarWash.Domain.UnitTests;

public class ClientTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var fullName = "Иванов Иван Иванович";
        var phoneNumber = "+79991234567";

        var client = new Client(fullName, phoneNumber);

        Assert.Equal(fullName, client.FullName);
        Assert.Equal(phoneNumber, client.PhoneNumber);
        Assert.NotEqual(Guid.Empty, client.Id);
    }

    [Fact]
    public void Constructor_Throws_WhenFullNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Client(null!, "+79991234567"));
    }

    [Fact]
    public void Constructor_Throws_WhenFullNameIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Client("", "+79991234567"));
    }

}