using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.EfCore.Configurations;

/// <summary>
/// Настройка таблицы автомобилей
/// </summary>
public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.LicensePlate).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Brand).HasMaxLength(100);

        builder.Property<int?>("ClientId");
        builder.HasOne<Client>()
            .WithMany(c => c.Cars)
            .HasForeignKey("ClientId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(DataSeed.Cars.Select(car => new
        {
            car.Id,
            car.LicensePlate,
            car.Brand,
            ClientId = DataSeed.Clients.FirstOrDefault(c => c.Cars.Contains(car))?.Id
        }));
    }
}
