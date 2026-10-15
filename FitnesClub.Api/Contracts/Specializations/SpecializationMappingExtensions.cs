using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Specializations;

public static class SpecializationMappingExtensions
{
    public static SpecializationDto ToDto(this Specialization s) => new(
        s.Id,
        s.Name
    );

    public static Specialization ToEntity(this SpecializationCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        Name = dto.Name
    };

    public static void UpdateEntity(this SpecializationCreateUpdateDto dto, Specialization s)
    {
        s.Name = dto.Name;
    }
}