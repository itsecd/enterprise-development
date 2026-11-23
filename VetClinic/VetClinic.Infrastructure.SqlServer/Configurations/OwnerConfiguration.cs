using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица владельцев, при удалении владельца удаляются его питомцы
/// </summary>
public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.FullName).IsRequired();
        builder.Property(o => o.Address).HasMaxLength(250).IsRequired();
        builder.Property(o => o.Phone).HasMaxLength(20).IsRequired();

        builder.HasMany(o => o.Pets)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
