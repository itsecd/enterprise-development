using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Museum.Infrastructure.MySql.DependencyInjection;

namespace Museum.Infrastructure.MySql;

/// <summary>
/// Создаёт контекст для инструментов миграций (dotnet ef). Подключение к базе при этом не выполняется
/// </summary>
public sealed class MuseumDbContextFactory : IDesignTimeDbContextFactory<MuseumDbContext>
{
    /// <summary>
    /// Создаёт контекст со строкой подключения-заглушкой
    /// </summary>
    public MuseumDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<MuseumDbContext>()
            .UseMySql("Server=localhost;Database=museum;User ID=root;Password=design", MySqlServiceCollectionExtensions.ServerVersion)
            .Options;

        return new MuseumDbContext(options);
    }
}
