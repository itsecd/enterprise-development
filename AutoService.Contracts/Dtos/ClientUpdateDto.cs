namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных клиента.
/// </summary>
public class ClientUpdateDto
{
    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public required string Phone { get; set; }
}
