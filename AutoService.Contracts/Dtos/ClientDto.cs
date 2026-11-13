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
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}