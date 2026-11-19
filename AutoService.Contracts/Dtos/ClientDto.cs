namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для передачи данных о клиенте.
/// </summary>
public class ClientDto
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
