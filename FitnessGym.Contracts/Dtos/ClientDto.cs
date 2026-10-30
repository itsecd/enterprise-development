namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о клиенте фитнес-клуба
/// </summary>
public record ClientDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateOnly BirthDate,
    string Phone,
    DateOnly SubscriptionStart,
    DateOnly SubscriptionEnd);