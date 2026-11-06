using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Domain.Models;

/// <summary>
/// Водитель транспортного средства
/// </summary>
public class Driver
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество (может отсутствовать)
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Стаж вождения в годах
    /// </summary>
    public int Experience { get; set; }

    /// <summary>
    /// Категория водительских прав
    /// </summary>
    public LicenseCategory LicenseCategory { get; set; }
}