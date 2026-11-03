using DryCleaning.Domain;
using DryCleaning.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DryCleaning.Infrastructure.EfCore.Data.Configurations;

/// <summary>
/// Настройка таблицы клиентов 
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasData(new TestData().Clients);
    }
}