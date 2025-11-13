using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.Persistence.Configurations;

public class OrderServiceConfiguration : IEntityTypeConfiguration<OrderService>
{
    public void Configure(EntityTypeBuilder<OrderService> builder)
    {
        builder.HasKey(os => new { os.OrderId, os.ServiceId });

        builder.HasOne(os => os.Order)
               .WithMany(o => o.OrderServices)
               .HasForeignKey(os => os.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(os => os.Service)
               .WithMany()
               .HasForeignKey(os => os.ServiceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}