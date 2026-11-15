using Museum.Shared;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о выставке для ответа API
/// </summary>
public sealed class ExhibitionDto
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Тематика выставки
    /// </summary>
    public ExhibitionTheme Theme { get; init; }

    /// <summary>
    /// Номер зала
    /// </summary>
    public int HallNumber { get; init; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public DateTime EndDate { get; init; }
}
