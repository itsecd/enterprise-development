using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга тренера в базу данных
/// </summary>
public class TrainerConfiguration : PersonConfiguration, IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Experience).IsRequired();
        builder.HasOne(t => t.Specialization)
            .WithMany()
            .HasForeignKey("SpecializationId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}