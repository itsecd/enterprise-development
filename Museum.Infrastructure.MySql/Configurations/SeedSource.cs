using Museum.Domain;

namespace Museum.Infrastructure.MySql.Configurations;

/// <summary>
/// Единый набор начальных данных для настроек таблиц
/// </summary>
internal static class SeedSource
{
    /// <summary>
    /// Данные из DataSeeder, которые попадут в миграцию
    /// </summary>
    public static readonly DataSeeder Data = new();
}
