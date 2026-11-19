namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата аналитики по повторным клиентам.
/// </summary>
public class RepeatClientReportDto
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string FullName { get; set; }
}
