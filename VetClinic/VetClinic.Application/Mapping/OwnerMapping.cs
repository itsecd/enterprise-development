using VetClinic.Contracts.Dto.Owners;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между владельцем и его DTO
/// </summary>
public static class OwnerMapping
{
    /// <summary>
    /// Преобразовать владельца в DTO для чтения
    /// </summary>
    /// <param name="entity">Владелец</param>
    public static OwnerDto ToDto(this Owner entity) => new()
    {
        Id = entity.Id,
        FullName = entity.FullName,
        Address = entity.Address,
        Phone = entity.Phone
    };

    /// <summary>
    /// Преобразовать владельца в DTO с количеством питомцев
    /// </summary>
    /// <param name="entity">Владелец</param>
    public static OwnerPetsCountDto ToDtoWithPetsCount(this Owner entity) => new()
    {
        Id = entity.Id,
        FullName = entity.FullName,
        PetsCount = entity.Pets.Count
    };

    /// <summary>
    /// Создать владельца из DTO, идентификатор назначает репозиторий
    /// </summary>
    /// <param name="dto">Данные владельца</param>
    public static Owner ToEntity(this OwnerEditDto dto) => new()
    {
        FullName = dto.FullName,
        Address = dto.Address,
        Phone = dto.Phone
    };

    /// <summary>
    /// Перенести данные из DTO в существующего владельца
    /// </summary>
    /// <param name="entity">Изменяемый владелец</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Owner entity, OwnerEditDto dto)
    {
        entity.FullName = dto.FullName;
        entity.Address = dto.Address;
        entity.Phone = dto.Phone;
    }
}
