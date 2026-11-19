namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата аналитики по популярным работам.
/// </summary>
public class TopWorkReportDto
{
    /// <summary>
    /// Название работы.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Количество обращений.
    /// </summary>
    public int Count { get; set; }
}
