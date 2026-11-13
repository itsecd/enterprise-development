using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Configurations;

/// <summary>Настройка таблицы и связей Hall.</summary>
public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.ToTable("Halls");
        builder.Property(entity => entity.Name).HasMaxLength(200);
    }
}

