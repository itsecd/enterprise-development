using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.EfCore.Configurations;

/// <summary>
/// Настройка таблицы клиентов
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FullName).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Phone).HasMaxLength(30);

        builder.HasData(DataSeed.Clients.Select(c => new
        {
            c.Id,
            c.FullName,
            c.Phone
        }));
    }
}
