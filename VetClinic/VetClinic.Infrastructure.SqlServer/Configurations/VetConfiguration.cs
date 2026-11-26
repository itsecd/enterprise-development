using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица ветеринаров, номер паспорта уникален
/// </summary>
public class VetConfiguration : IEntityTypeConfiguration<Vet>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.PassportNumber).HasMaxLength(11).IsRequired();
        builder.HasIndex(v => v.PassportNumber).IsUnique();
        builder.Property(v => v.FullName).IsRequired();

        builder.HasOne(v => v.Specialization)
            .WithMany()
            .HasForeignKey(v => v.SpecializationId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
