namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата аналитики по клиентам механика.
/// </summary>
public class ClientMechanicReportDto
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public required string Phone { get; set; }
}
