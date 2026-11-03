using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Members;

/// <summary>
/// Информация о зарегистрированном клиенте фитнес-клуба.
/// </summary>
/// <param name="Id">Уникальный идентификатор клиента.</param>
/// <param name="PassportNumber">Серия и номер паспорта клиента.</param>
/// <param name="FirstName">Имя клиента.</param>
/// <param name="LastName">Фамилия клиента.</param>
/// <param name="Gender">Пол клиента.</param>
/// <param name="BirthDate">Дата рождения клиента.</param>
/// <param name="PhoneNumber">Контактный номер телефона.</param>
/// <param name="MembershipStartDate">Дата и время начала действия абонемента.</param>
/// <param name="MembershipEndDate">Дата и время окончания действия абонемента.</param>
public record MemberDto(
    Guid Id,
    string PassportNumber,
    string FirstName,
    string LastName,
    Gender Gender,
    DateOnly BirthDate,
    string PhoneNumber,
    DateTime MembershipStartDate,
    DateTime MembershipEndDate
);