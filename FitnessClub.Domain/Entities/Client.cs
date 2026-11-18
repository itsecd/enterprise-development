namespace FitnessClub.Domain.Entities;

/// <summary>
/// Класс клиента
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Телефон клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала действия абонемента
    /// </summary>
    public required DateTime SubscriptionStart { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента
    /// </summary>
    public required DateTime SubscriptionEnd { get; set; }
}
