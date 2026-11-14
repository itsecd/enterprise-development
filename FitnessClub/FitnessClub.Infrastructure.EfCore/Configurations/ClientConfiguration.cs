using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Configurations;

/// <summary>Настройка таблицы и связей Client.</summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.HasBaseType((Type?)null);
        builder.ToTable("Clients");
        builder.Property(entity => entity.Phone).HasMaxLength(32);
        builder.Property(entity => entity.SubscriptionStartDate).HasColumnType("date");
        builder.Property(entity => entity.SubscriptionEndDate).HasColumnType("date");
        builder.Property(entity => entity.PassportNumber).HasMaxLength(32);
        builder.HasIndex(entity => entity.PassportNumber).IsUnique();
        builder.Property(entity => entity.FullName).HasMaxLength(200);
        builder.Property(entity => entity.BirthDate).HasColumnType("date");
    }
}

