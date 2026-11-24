using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица пользователей, имя для входа уникально
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
    }
}
