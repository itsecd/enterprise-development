using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга клиента в базу данных
/// </summary>
public class ClientConfiguration : PersonConfiguration, IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.SubscriptionStart);
        builder.Property(c => c.SubscriptionEnd);
    }
}