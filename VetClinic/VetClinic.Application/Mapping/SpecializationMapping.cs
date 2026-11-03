using VetClinic.Contracts.Dto.Specializations;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между специализацией врача и её DTO
/// </summary>
public static class SpecializationMapping
{
    /// <summary>
    /// Преобразовать специализацию в DTO для чтения
    /// </summary>
    /// <param name="entity">Специализация</param>
    public static SpecializationDto ToDto(this Specialization entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        FocusSpecies = entity.FocusSpecies
    };

    /// <summary>
    /// Создать специализацию из DTO, идентификатор назначает репозиторий
    /// </summary>
    /// <param name="dto">Данные специализации</param>
    public static Specialization ToEntity(this SpecializationEditDto dto) => new()
    {
        Name = dto.Name,
        FocusSpecies = dto.FocusSpecies
    };

    /// <summary>
    /// Перенести данные из DTO в существующую специализацию
    /// </summary>
    /// <param name="entity">Изменяемая специализация</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Specialization entity, SpecializationEditDto dto)
    {
        entity.Name = dto.Name;
        entity.FocusSpecies = dto.FocusSpecies;
    }
}
