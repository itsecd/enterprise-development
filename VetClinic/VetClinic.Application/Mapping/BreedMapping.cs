using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между породой и её DTO
/// </summary>
public static class BreedMapping
{
    /// <summary>
    /// Преобразовать породу в DTO для чтения
    /// </summary>
    /// <param name="entity">Порода</param>
    public static BreedDto ToDto(this Breed entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Species = entity.Species
    };

    /// <summary>
    /// Создать породу из DTO, идентификатор назначает репозиторий
    /// </summary>
    /// <param name="dto">Данные породы</param>
    public static Breed ToEntity(this BreedEditDto dto) => new()
    {
        Name = dto.Name,
        Species = dto.Species
    };

    /// <summary>
    /// Перенести данные из DTO в существующую породу
    /// </summary>
    /// <param name="entity">Изменяемая порода</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Breed entity, BreedEditDto dto)
    {
        entity.Name = dto.Name;
        entity.Species = dto.Species;
    }
}
