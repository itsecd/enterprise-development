using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга тренера в базу данных
/// </summary>
public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.Property(t => t.Experience).IsRequired();
        builder.HasOne(t => t.Specialization)
            .WithMany()
            .HasForeignKey("SpecializationId")
            .OnDelete(DeleteBehavior.Restrict);
        builder.Navigation(trainer => trainer.Specialization).AutoInclude();
    }
}