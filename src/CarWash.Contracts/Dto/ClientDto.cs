namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные клиента
/// </summary>
public class ClientDto
{
    /// <summary>
    /// ID клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; set; }
}
