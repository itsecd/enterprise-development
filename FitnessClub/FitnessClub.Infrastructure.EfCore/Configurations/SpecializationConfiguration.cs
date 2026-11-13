using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Configurations;

/// <summary>Настройка таблицы и связей Specialization.</summary>
public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.ToTable("Specializations");
        builder.Property(entity => entity.Name).HasMaxLength(200);
        builder.Property(entity => entity.Description).HasMaxLength(2000);
    }
}

