using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Trainers;

public record TrainerDto(
    Guid Id,
    string PassportNumber,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    Gender Gender,
    DateOnly BirthDate,
    Guid SpecializationId,
    int WorkExperienceYears
);
