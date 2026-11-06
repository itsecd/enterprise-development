
using FitnessClub.Domain.Enums;

namespace FitnessClub.Api.Dtos.Clients;

/// <summary>
/// Данные клиента для передачи через API.
/// </summary>
public record ClientDto(
    int Id,
    string PassportNumber,
    string FullName,
    Gender Gender,
    DateOnly BirthDate,
    string PhoneNumber,
    DateOnly SubscriptionStartDate,
    DateOnly SubscriptionEndDate);
