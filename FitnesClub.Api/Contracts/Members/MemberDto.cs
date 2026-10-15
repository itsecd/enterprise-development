using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Members;

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
