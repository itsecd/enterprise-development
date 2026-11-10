namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о популярности выставки
/// </summary>
public sealed class TopExhibitionDto
{
    /// <summary>
    /// Информация о выставке
    /// </summary>
    public required ExhibitionDto Exhibition { get; init; }

    /// <summary>
    /// Количество посетителей выставки
    /// </summary>
    public int VisitorCount { get; init; }
}
