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
    /// Стаж работы, г.
    /// </summary>
    public int Experience { get; set; }
}
