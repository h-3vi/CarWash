namespace CarWash.Domain.Entities;

public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private Service() { } 
   public Service(string name, string description, decimal price)
    {
        if (name == null || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название услуги не может быть пустым", nameof(name));

        if (description == null)
            throw new ArgumentException(nameof(description));

        if (price < 0)
            throw new ArgumentException("Цена не может быть отрицательной", nameof(price));

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description.Trim();
        Price = price;
    }
}