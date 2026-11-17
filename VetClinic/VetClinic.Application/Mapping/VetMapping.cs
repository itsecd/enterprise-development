using VetClinic.Contracts.Dto.Vets;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между ветеринаром и его DTO
/// </summary>
public static class VetMapping
{
    /// <summary>
    /// Преобразовать ветеринара в DTO для чтения
    /// </summary>
    /// <param name="entity">Ветеринар</param>
    public static VetDto ToDto(this Vet entity) => new()
    {
        Id = entity.Id,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        BirthYear = entity.BirthYear,
        SpecializationId = entity.SpecializationId,
        WorkExperienceYears = entity.WorkExperienceYears
    };

    /// <summary>
    /// Создать ветеринара из DTO без связей, связи назначает сервис
    /// </summary>
    /// <param name="dto">Данные ветеринара</param>
    public static Vet ToEntity(this VetEditDto dto) => new()
    {
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        BirthYear = dto.BirthYear,
        WorkExperienceYears = dto.WorkExperienceYears
    };

    /// <summary>
    /// Перенести простые поля из DTO в существующего ветеринара, связи назначает сервис
    /// </summary>
    /// <param name="entity">Изменяемый ветеринар</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Vet entity, VetEditDto dto)
    {
        entity.PassportNumber = dto.PassportNumber;
        entity.FullName = dto.FullName;
        entity.BirthYear = dto.BirthYear;
        entity.WorkExperienceYears = dto.WorkExperienceYears;
    }
}
