using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Members;

/// <summary>
/// Данные для создания или обновления информации о клиенте фитнес-клуба.
/// </summary>
/// <param name="PassportNumber">Серия и номер паспорта клиента.</param>
/// <param name="FirstName">Имя клиента.</param>
/// <param name="LastName">Фамилия клиента.</param>
/// <param name="Gender">Пол клиента.</param>
/// <param name="BirthDate">Дата рождения клиента.</param>
/// <param name="PhoneNumber">Контактный номер телефона.</param>
/// <param name="MembershipStartDate">Дата и время начала действия абонемента.</param>
/// <param name="MembershipEndDate">Дата и время окончания действия абонемента.</param>
public record MemberCreateUpdateDto(
    string PassportNumber,
    string FirstName,
    string LastName,
    Gender Gender,
    DateOnly BirthDate,
    string PhoneNumber,
    DateTime MembershipStartDate,
    DateTime MembershipEndDate
);