namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент фитнес-клуба.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата начала абонемента.
    /// </summary>
    public required DateOnly SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания абонемента.
    /// </summary>
    public required DateOnly SubscriptionEndDate { get; set; }
}