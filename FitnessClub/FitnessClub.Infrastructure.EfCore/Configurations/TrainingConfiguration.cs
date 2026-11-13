using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Configurations;

/// <summary>Настройка таблицы и связей Training.</summary>
public class TrainingConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.ToTable("Trainings");
        builder.Property(entity => entity.StartTime).HasColumnType("timestamp with time zone");
        builder.HasOne(entity => entity.Client).WithMany()
            .HasForeignKey(entity => entity.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(entity => entity.Trainer).WithMany()
            .HasForeignKey(entity => entity.TrainerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(entity => entity.Hall).WithMany()
            .HasForeignKey(entity => entity.HallId).OnDelete(DeleteBehavior.Restrict);
    }
}

