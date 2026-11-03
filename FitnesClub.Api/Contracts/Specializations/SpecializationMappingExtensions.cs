using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Specializations;

/// <summary>
/// Методы расширения для преобразования между доменной сущностью <see cref="Specialization"/> и DTO специализаций.
/// </summary>
public static class SpecializationMappingExtensions
{
    /// <summary>
    /// Преобразует доменную сущность специализации в объект передачи данных <see cref="SpecializationDto"/>.
    /// </summary>
    /// <param name="s">Сущность специализации тренера.</param>
    /// <returns>DTO с идентификатором и наименованием направления специализации.</returns>
    public static SpecializationDto ToDto(this Specialization s) => new(
        s.Id,
        s.Name
    );

    /// <summary>
    /// Создает новый экземпляр доменной сущности <see cref="Specialization"/> на основе входного DTO.
    /// </summary>
    /// <param name="dto">Входные данные для создания специализации.</param>
    /// <returns>Новая сущность специализации с новым идентификатором <see cref="Guid"/>.</returns>
    public static Specialization ToEntity(this SpecializationCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        Name = dto.Name
    };

    /// <summary>
    /// Обновляет наименование существующей специализации значениями из переданного DTO.
    /// </summary>
    /// <param name="dto">DTO с актуальным наименованием направления.</param>
    /// <param name="s">Существующая сущность специализации, поле названия которой будет перезаписано.</param>
    public static void UpdateEntity(this SpecializationCreateUpdateDto dto, Specialization s)
    {
        s.Name = dto.Name;
    }
}