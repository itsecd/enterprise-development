using VetClinic.Contracts.Dto.Pets;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между питомцем и его DTO
/// </summary>
public static class PetMapping
{
    /// <summary>
    /// Преобразовать питомца в DTO для чтения
    /// </summary>
    /// <param name="entity">Питомец</param>
    public static PetDto ToDto(this Pet entity) => new()
    {
        Id = entity.Id,
        Nickname = entity.Nickname,
        Species = entity.Species,
        BreedId = entity.Breed?.Id,
        BirthDate = entity.BirthDate,
        Weight = entity.Weight,
        OwnerId = entity.Owner?.Id ?? 0
    };

    /// <summary>
    /// Создать питомца из DTO без связей, связи назначает сервис
    /// </summary>
    /// <param name="dto">Данные питомца</param>
    public static Pet ToEntity(this PetEditDto dto) => new()
    {
        Nickname = dto.Nickname,
        Species = dto.Species,
        BirthDate = dto.BirthDate,
        Weight = dto.Weight
    };

    /// <summary>
    /// Перенести простые поля из DTO в существующего питомца, связи назначает сервис
    /// </summary>
    /// <param name="entity">Изменяемый питомец</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Pet entity, PetEditDto dto)
    {
        entity.Nickname = dto.Nickname;
        entity.Species = dto.Species;
        entity.BirthDate = dto.BirthDate;
        entity.Weight = dto.Weight;
    }
}
