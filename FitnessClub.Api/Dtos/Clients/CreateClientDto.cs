
using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Api.Dtos.Clients;

/// <summary>
/// Данные для создания или изменения клиента.
/// </summary>
public class CreateClientDto
{
    /// <summary>
    /// Номер паспорта клиента.
    /// </summary>
    [Required]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество клиента.
    /// </summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Пол клиента.
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Контактный номер телефона клиента.
    /// </summary>
    [Required]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата начала действия абонемента.
    /// </summary>
    public DateOnly SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента.
    /// </summary>
    public DateOnly SubscriptionEndDate { get; set; }
}
