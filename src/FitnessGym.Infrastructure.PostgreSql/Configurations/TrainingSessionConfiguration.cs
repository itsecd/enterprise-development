using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Infrastructure.PostgreSql.Configurations;

public class TrainingSessionConfiguration : IEntityTypeConfiguration<TrainingSession>
{
    public void Configure(EntityTypeBuilder<TrainingSession> builder)
    {
        builder.ToTable("TrainingSessions", table => table.HasCheckConstraint(
            "CK_TrainingSessions_TimeRange", "\"EndsAt\" IS NULL OR \"EndsAt\" > \"StartsAt\""));
        builder.HasKey(session => session.Id);
        builder.Property(session => session.Id).HasIdentityOptions(startValue: 100);
        // Занятия задаются по местному времени клуба, без часового пояса.
        builder.Property(session => session.StartsAt).HasColumnType("timestamp without time zone");
        builder.Property(session => session.EndsAt).HasColumnType("timestamp without time zone");
        builder.Property(session => session.HallName).IsRequired();
        builder.HasOne(session => session.Client).WithMany()
            .HasForeignKey(session => session.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(session => session.Trainer).WithMany()
            .HasForeignKey(session => session.TrainerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(session => new { session.HallName, session.StartsAt });
    }
}
