using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Status).HasDefaultValue(OrderStatus.Pending);

        builder.HasOne(o => o.Client)
               .WithMany()
               .HasForeignKey(o => o.ClientId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(o => o.Car)
               .WithMany()
               .HasForeignKey(o => o.CarId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(o => o.OrderServices)
               .WithOne(os => os.Order)
               .HasForeignKey(os => os.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}