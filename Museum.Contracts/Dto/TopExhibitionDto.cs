namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о популярности выставки
/// </summary>
public sealed class TopExhibitionDto
{
    /// <summary>
    /// Информация о выставке
    /// </summary>
    public ExhibitionDto Exhibition { get; init; } = null!;

    /// <summary>
    /// Количество посетителей выставки
    /// </summary>
    public int VisitorCount { get; init; }
}
