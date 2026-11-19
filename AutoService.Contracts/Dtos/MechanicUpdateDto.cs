namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных механика.
/// </summary>
public class MechanicUpdateDto
{
    /// <summary>
    /// ФИО механика.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// Специализация.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы.
    /// </summary>
    public int Experience { get; set; }
}
