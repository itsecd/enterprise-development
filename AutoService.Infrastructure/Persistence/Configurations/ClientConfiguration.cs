using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация таблицы клиентов
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <summary>
    /// Настроить отображение клиента на таблицу базы данных
    /// </summary>
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FullName).HasMaxLength(200);
        builder.Property(c => c.Phone).HasMaxLength(20);
        builder.HasIndex(c => c.Phone).IsUnique();

        builder.HasMany(c => c.Cars)
            .WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
