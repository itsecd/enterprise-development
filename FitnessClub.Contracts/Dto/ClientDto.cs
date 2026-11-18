using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Клиент (read-модель)
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Телефон
    /// </summary>
    public string Phone { get; init; } = string.Empty;

    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    public DateTime SubscriptionStart { get; init; }

    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    public DateTime SubscriptionEnd { get; init; }
}