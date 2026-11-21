namespace FitnessClub.Domain.Entities;
/// <summary>
/// Класс данных для клиента фитнес клуба
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Начало абонемента
    /// </summary>
    public DateOnly SubscriptionStart { get; set; }
    /// <summary>
    /// Окончание абонемента
    /// </summary>
    public DateOnly SubscriptionEnd { get; set; }
}