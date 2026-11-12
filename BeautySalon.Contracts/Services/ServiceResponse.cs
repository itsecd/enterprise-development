using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Contracts.Services;

/// <summary>
/// Сведения об услуге
/// </summary>
public sealed class ServiceResponse : IResourceResponse
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    public string? Category { get; init; }

    /// <summary>
    /// Стоимость в рублях
    /// </summary>
    public required decimal Price { get; init; }

    /// <summary>
    /// Продолжительность в минутах
    /// </summary>
    public required int DurationMinutes { get; init; }
}
