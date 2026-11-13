namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных клиента.
/// </summary>
public class ClientUpdateDto
{
    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}