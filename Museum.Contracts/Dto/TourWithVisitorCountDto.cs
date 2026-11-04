namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о количестве участников экскурсии
/// </summary>
public sealed class TourWithVisitorCountDto
{
    /// <summary>
    /// Информация об экскурсии
    /// </summary>
    public TourDto Tour { get; init; } = null!;

    /// <summary>
    /// Количество участников экскурсии
    /// </summary>
    public int VisitorCount { get; init; }
}
