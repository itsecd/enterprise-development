namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных механика.
/// </summary>
public class MechanicUpdateDto
{
    /// <summary>
    /// ФИО механика.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string Passport { get; set; } = string.Empty;

    /// <summary>
    /// Специализация.
    /// </summary>
    public string Specialization { get; set; } = string.Empty;

    /// <summary>
    /// Стаж работы.
    /// </summary>
    public int Experience { get; set; }
}