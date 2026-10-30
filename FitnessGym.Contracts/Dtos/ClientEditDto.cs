using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания или обновления клиента
/// </summary>
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