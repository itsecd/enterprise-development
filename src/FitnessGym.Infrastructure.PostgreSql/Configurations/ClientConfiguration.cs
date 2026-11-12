using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Infrastructure.PostgreSql.Configurations;

/// <summary>
/// Клиенты хранятся отдельно от тренеров, с собственной последовательностью идентификаторов.
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasBaseType((Type?)null);
        builder.ToTable("Clients", table => table.HasCheckConstraint(
            "CK_Clients_SubscriptionDates", "\"SubscriptionEnd\" >= \"SubscriptionStart\""));
        builder.HasKey(client => client.Id);
        builder.Property(client => client.Id).HasIdentityOptions(startValue: 100);
        builder.Property(client => client.PassportNumber).IsRequired();
        builder.Property(client => client.FullName).IsRequired();
        builder.Property(client => client.Phone).IsRequired();
        builder.HasIndex(client => client.SubscriptionEnd);
    }
}
