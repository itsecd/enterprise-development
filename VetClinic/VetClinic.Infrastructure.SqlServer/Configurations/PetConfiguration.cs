using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица питомцев, при удалении породы она у питомца сбрасывается
/// </summary>
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nickname).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Species).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(p => p.Breed)
            .WithMany()
            .HasForeignKey(p => p.BreedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
