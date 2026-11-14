using FitnessClub.Contracts.Dtos;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Выполняет преобразование специализации в DTO.
/// </summary>
public static class SpecializationMapping
{
    /// <summary>
    /// Преобразует специализацию в DTO.
    /// </summary>
    /// <param name="specialization">Специализация тренера.</param>
    /// <returns>DTO специализации.</returns>
    public static SpecializationDto ToDto(this Specialization specialization)
    {
        ArgumentNullException.ThrowIfNull(specialization);

        return new SpecializationDto
        {
            Id = specialization.Id,
            Name = specialization.Name,
            Description = specialization.Description
        };
    }
}