namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о клиенте фитнес-клуба
/// </summary>
/// <param name="Id">Уникальный идентификатор клиента</param>
/// <param name="PassportNumber">Номер паспорта клиента</param>
/// <param name="FullName">ФИО клиента</param>
/// <param name="Gender">Пол клиента (Male/Female)</param>
/// <param name="BirthDate">Дата рождения клиента</param>
/// <param name="Phone">Контактный телефон</param>
/// <param name="SubscriptionStart">Дата начала действия абонемента</param>
/// <param name="SubscriptionEnd">Дата окончания действия абонемента</param>
public record ClientDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateOnly BirthDate,
    string Phone,
    DateOnly SubscriptionStart,
    DateOnly SubscriptionEnd);