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
    /// <param name="master">Мастер</param>
    /// <returns>DTO мастера</returns>
    public static MasterDto ToDto(this Master master) =>
        new(master.Id, master.FullName, master.Gender, master.PassportNumber,
            master.Specialization, master.WorkExperience);

    /// <summary>
    /// Создаёт нового мастера из DTO (идентификатор назначит репозиторий)
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Новый мастер</returns>
    public static Master ToEntity(this MasterEditDto dto) =>
        new()
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            WorkExperience = dto.WorkExperience
        };

    /// <summary>
    /// Переносит данные из DTO в существующего мастера
    /// </summary>
    /// <param name="master">Обновляемый мастер</param>
    /// <param name="dto">Новые данные</param>
    public static void UpdateEntity(this Master master, MasterEditDto dto)
    {
        master.FullName = dto.FullName;
        master.Gender = dto.Gender;
        master.PassportNumber = dto.PassportNumber;
        master.Specialization = dto.Specialization;
        master.WorkExperience = dto.WorkExperience;
    }
}
