namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент фитнес-клуба.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    /// <example>+79991234567</example>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала действия абонемента.
    /// </summary>
    public required DateTime SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента.
    /// </summary>
    public required DateTime SubscriptionEndDate { get; set; }
}