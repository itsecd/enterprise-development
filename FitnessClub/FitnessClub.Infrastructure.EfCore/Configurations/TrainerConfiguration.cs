using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Configurations;

/// <summary>Настройка таблицы и связей Trainer.</summary>
public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.HasBaseType((Type?)null);
        builder.ToTable("Trainers");
        builder.HasOne(entity => entity.Specialization).WithMany()
            .HasForeignKey(entity => entity.SpecializationId).OnDelete(DeleteBehavior.Restrict);
        builder.Property(entity => entity.PassportNumber).HasMaxLength(32);
        builder.HasIndex(entity => entity.PassportNumber).IsUnique();
        builder.Property(entity => entity.FullName).HasMaxLength(200);
        builder.Property(entity => entity.BirthDate).HasColumnType("date");
    }
}

