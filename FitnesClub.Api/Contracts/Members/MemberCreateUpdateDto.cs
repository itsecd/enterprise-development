using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Members;

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