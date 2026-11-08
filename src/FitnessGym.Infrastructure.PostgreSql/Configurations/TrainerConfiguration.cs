using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Infrastructure.PostgreSql.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasBaseType((Type?)null);
        builder.ToTable("Trainers", table => table.HasCheckConstraint(
            "CK_Trainers_ExperienceYears", "\"ExperienceYears\" BETWEEN 0 AND 100"));
        builder.HasKey(trainer => trainer.Id);
        builder.Property(trainer => trainer.Id).HasIdentityOptions(startValue: 100);
        builder.Property(trainer => trainer.PassportNumber).IsRequired();
        builder.Property(trainer => trainer.FullName).IsRequired();
        builder.HasOne(trainer => trainer.Specialization).WithMany()
            .HasForeignKey(trainer => trainer.SpecializationId).OnDelete(DeleteBehavior.Restrict);
    }
}
