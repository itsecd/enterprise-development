using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO водителя транспортной компании
/// </summary>
public class DriverDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Серия и номер паспорта
    /// </summary>
    /// <example>1234 567890</example>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// ФИО водителя
    /// </summary>
    /// <example>Петров Пётр Петрович</example>
    public required string FullName { get; init; }

    /// <summary>
    /// Стаж вождения в годах
    /// </summary>
    public int? Experience { get; init; }

    /// <summary>
    /// Водительское удостоверение
    /// </summary>
    public DrivingLicense? License { get; init; }
}