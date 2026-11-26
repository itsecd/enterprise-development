using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;
/// <summary>
/// Преобразует данные специализаций
/// </summary>
public static class SpecializationMapper
{
    /// <summary>
    /// Преобразует специализацию в DTO
    /// </summary>
    public static SpecializationDto ToDto(this Specialization specialization) =>
        new(
            Id: specialization.Id,
            Name: specialization.Name);
    /// <summary>
    /// Создаёт специализацию из DTO
    /// </summary>
    public static Specialization ToEntity(this SpecializationEditDto specializationEditDto) =>
        new() { Name = specializationEditDto.Name };
    /// <summary>
    /// Обновляет специализацию данными из DTO
    /// </summary>
    public static void UpdateEntity(this Specialization specialization, SpecializationEditDto specializationEditDto)
    {
        specialization.Name = specializationEditDto.Name;
    }
}