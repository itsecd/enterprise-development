using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

/// <summary>
/// Водитель транспортной компании
/// </summary>
public class Driver
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Серия и номер паспорта
    /// </summary>
    /// <example>1234 567890</example>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО водителя
    /// </summary>
    /// <example>Петров Пётр Петрович</example>
    public required string FullName { get; set; }

    /// <summary>
    /// Стаж вождения в годах
    /// </summary>
    public required int Experience { get; set; }

    /// <summary>
    /// Водительское удостоверение
    /// </summary>
    public required DrivingLicense License { get; set; }
}