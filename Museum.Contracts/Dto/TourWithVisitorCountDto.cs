namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о количестве участников экскурсии
/// </summary>
public sealed class TourWithVisitorCountDto
{
    /// <summary>
    /// Информация об экскурсии
    /// </summary>
    public required TourDto Tour { get; init; }

    /// <summary>
    /// Количество участников экскурсии
    /// </summary>
    public int VisitorCount { get; init; }
}
