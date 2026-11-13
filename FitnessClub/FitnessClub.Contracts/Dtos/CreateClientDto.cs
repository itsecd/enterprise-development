using FitnessClub.Contracts.Validation;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для создания клиента фитнес-клуба.
/// </summary>
public class CreateClientDto
{
    /// <summary>
    /// Номер паспорта клиента.
    /// </summary>
    [Required]
    [MaxLength(32)]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО клиента.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public required string FullName { get; set; }

    /// <summary>
    /// Пол клиента.
    /// </summary>
    [Required]
    [EnumDataType(typeof(GenderDto))]
    public GenderDto? Gender { get; set; }

    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    [ValidDate(AllowFuture = false)]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    [Required]
    [Phone]
    [MaxLength(32)]
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала действия абонемента.
    /// </summary>
    [ValidDate]
    public DateTime SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента.
    /// </summary>
    [ValidDate]
    [DateNotBefore(nameof(SubscriptionStartDate))]
    public DateTime SubscriptionEndDate { get; set; }
}