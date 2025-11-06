using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.Persistence.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Brand).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Model).IsRequired().HasMaxLength(50);
        builder.Property(c => c.LicensePlate).IsRequired().HasMaxLength(15);

        builder.HasOne(c => c.Client)
               .WithMany()
               .HasForeignKey(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}