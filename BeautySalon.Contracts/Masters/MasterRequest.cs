using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Masters;

/// <summary>
/// Запрос на создание или обновление мастера.
/// </summary>
public class MasterRequest
{
    /// <summary>
    /// ФИО.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы.
    /// </summary>
    public int ExperienceYears { get; set; }
}