using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Trainers;

public static class TrainerMappingExtensions
{
    public static TrainerDto ToDto(this Trainer t) => new (
        t.Id,
        t.FirstName,
        t.LastName,
        t.PassportNumber,
        t.PhoneNumber,
        t.Email,
        t.Gender,
        t.BirthDate,
        t.SpecializationId,
        t.WorkExperienceYears
    );

    public static Trainer ToEntity(this TrainersCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        PassportNumber = dto.PassportNumber,
        PhoneNumber = dto.PhoneNumber,
        Email = dto.Email,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        SpecializationId = dto.SpecializationId,
        WorkExperienceYears = dto.WorkExperienceYears,
        Specialization = null!
    };
    public static void UpdateEntity(this TrainersCreateUpdateDto dto, Trainer t)
    {
        t.FirstName = dto.FirstName;
        t.LastName = dto.LastName;
        t.PassportNumber = dto.PassportNumber;
        t.PhoneNumber = dto.PhoneNumber;
        t.Email = dto.Email;
        t.Gender = dto.Gender;
        t.BirthDate = dto.BirthDate;
        t.SpecializationId = dto.SpecializationId;
        t.WorkExperienceYears = dto.WorkExperienceYears;
    }
}

