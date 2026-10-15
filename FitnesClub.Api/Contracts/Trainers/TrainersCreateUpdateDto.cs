using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Trainers;

public record TrainersCreateUpdateDto(
    string FirstName,
    string LastName,
    string PassportNumber,
    string PhoneNumber,
    string? Email,
    Gender Gender,
    DateOnly BirthDate,
    Guid SpecializationId,
    int WorkExperienceYears
);
