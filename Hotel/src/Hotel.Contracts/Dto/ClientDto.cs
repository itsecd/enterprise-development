namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные клиента в ответе API
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public required string Citizenship { get; set; }
}
