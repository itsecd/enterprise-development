namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для передачи данных о механике.
/// </summary>
public class MechanicDto
{
    /// <summary>
    /// Идентификатор механика.
    /// </summary>
    public Guid Id { get; set; }

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
    /// Стаж работы, г.
    /// </summary>
    public int Experience { get; set; }
}