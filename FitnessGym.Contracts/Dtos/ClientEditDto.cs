using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания или обновления клиента
/// </summary>
/// <param name="PassportNumber">Номер паспорта (обязательное поле, до 20 символов)</param>
/// <param name="FullName">ФИО клиента (обязательное поле, до 200 символов)</param>
/// <param name="Gender">Пол клиента (Male/Female, обязательное поле)</param>
/// <param name="BirthDate">Дата рождения (обязательное поле)</param>
/// <param name="Phone">Контактный телефон (обязательное поле, валидный номер)</param>
/// <param name="SubscriptionStart">Дата начала абонемента (обязательное поле)</param>
/// <param name="SubscriptionEnd">Дата окончания абонемента (обязательное поле)</param>
public record ClientEditDto(
    [Required, MaxLength(20)]
    string PassportNumber,

    [Required, MaxLength(200)]
    string FullName,

    [Required]
    string Gender,

    [Required]
    DateOnly BirthDate,

    [Required, Phone, MaxLength(20)]
    string Phone,

    [Required]
    DateOnly SubscriptionStart,

    [Required]
    DateOnly SubscriptionEnd);