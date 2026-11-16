using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга человечка в базу данных
/// </summary>
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.PassportNumber).HasMaxLength(20).IsRequired();
        builder.Property(p => p.FullName).HasMaxLength(200).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(20);
        builder.Property(p => p.Gender).HasConversion<string>().HasMaxLength(20);
        builder.HasIndex(p => p.PassportNumber).IsUnique();

        builder.ToTable("people");
    }
}