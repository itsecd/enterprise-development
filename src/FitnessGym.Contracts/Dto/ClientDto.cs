using FitnessGym.Domain.Enums;

namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO клиента фитнес-клуба: данные в ответе сервера.
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>
    /// ФИО.
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly BirthDate { get; init; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string Phone { get; init; } = string.Empty;

    /// <summary>
    /// Дата начала абонемента.
    /// </summary>
    public DateOnly SubscriptionStart { get; init; }

    /// <summary>
    /// Дата окончания абонемента.
    /// </summary>
    public DateOnly SubscriptionEnd { get; init; }
}
