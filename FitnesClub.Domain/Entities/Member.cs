namespace FitnesClub.Domain.Entities;

/// <summary>
/// Клиент фитнес-клуба
/// </summary>
public class Member : Person
{
    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    public required DateTime MembershipStartDate { get; set; }

    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    public required DateTime MembershipEndDate { get; set; }

    /// <summary>
    /// Список записей клиента на занятия
    /// </summary>
    public List<Booking> Bookings { get; set; } = [];
}