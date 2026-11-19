namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата аналитики по механикам.
/// </summary>
public class MechanicReportDto
{
    /// <summary>
    /// Идентификатор механика.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО механика.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Специализация механика.
    /// </summary>
    public required string Specialization { get; set; }
}
