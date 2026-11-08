using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Infrastructure.PostgreSql.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("Specializations");
        builder.HasKey(specialization => specialization.Id);
        builder.Property(specialization => specialization.Id).HasIdentityOptions(startValue: 100);
        builder.Property(specialization => specialization.Name).IsRequired();
    }
}
