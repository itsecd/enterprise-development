namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные клиента, возвращаемые API.
/// </summary>
public class ClientDto
{
    /// <summary>
    /// ID клиента.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта клиента.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол клиента.
    /// </summary>
    /// <example>Male</example>
    public GenderDto Gender { get; set; }

    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    /// <example>+79991234567</example>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала действия абонемента.
    /// </summary>
    public DateTime SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента.
    /// </summary>
    public DateTime SubscriptionEndDate { get; set; }
}