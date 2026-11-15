using BeautySalon.Contracts.Dto;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Mapping;

/// <summary>
/// Преобразования между мастером и его DTO
/// </summary>
public static class MasterMapper
{
    /// <summary>
    /// Преобразует мастера в DTO для чтения
    /// </summary>
    public static MasterDto ToDto(this Master master) =>
        new(master.Id, master.FullName, master.Gender, master.PassportNumber,
            master.Specialization, master.WorkExperience);

    /// <summary>
    /// Создаёт нового мастера из DTO (идентификатор назначит репозиторий)
    /// </summary>
    public static Master ToEntity(this MasterEditDto dto) =>
        new()
        {
            FullName = dto.FullName,
            Gender = dto.Gender!.Value,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization!.Value,
            WorkExperience = dto.WorkExperience!.Value
        };

    /// <summary>
    /// Переносит данные из DTO в существующего мастера
    /// </summary>
    public static void UpdateEntity(this Master master, MasterEditDto dto)
    {
        master.FullName = dto.FullName;
        master.Gender = dto.Gender!.Value;
        master.PassportNumber = dto.PassportNumber;
        master.Specialization = dto.Specialization!.Value;
        master.WorkExperience = dto.WorkExperience!.Value;
    }
}