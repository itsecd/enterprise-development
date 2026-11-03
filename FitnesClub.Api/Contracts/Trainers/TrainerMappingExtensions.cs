using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Trainers;

/// <summary>
/// Методы расширения для преобразования между доменной сущностью <see cref="Trainer"/> и DTO тренеров.
/// </summary>
public static class TrainerMappingExtensions
{
    /// <summary>
    /// Преобразует доменную сущность тренера в объект передачи данных <see cref="TrainerDto"/>.
    /// </summary>
    /// <param name="t">Сущность тренера клуба.</param>
    /// <returns>DTO с полными данными тренера, включая специализацию и стаж работы.</returns>
    public static TrainerDto ToDto(this Trainer t) => new(
        t.Id,
        t.FirstName,
        t.LastName,
        t.PassportNumber,
        t.PhoneNumber,
        t.Email,
        t.Gender,
        t.BirthDate,
        t.SpecializationId,
        t.WorkExperienceYears
    );

    /// <summary>
    /// Создает новый экземпляр доменной сущности <see cref="Trainer"/> на основе входного DTO.
    /// </summary>
    /// <param name="dto">Входные данные для создания тренера.</param>
    /// <returns>Новая сущность тренера с новым идентификатором <see cref="Guid"/>.</returns>
    public static Trainer ToEntity(this TrainersCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        PassportNumber = dto.PassportNumber,
        PhoneNumber = dto.PhoneNumber,
        Email = dto.Email,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        SpecializationId = dto.SpecializationId,
        WorkExperienceYears = dto.WorkExperienceYears
    };

    /// <summary>
    /// Обновляет персональные данные, контакты и специализацию тренера значениями из переданного DTO.
    /// </summary>
    /// <param name="dto">DTO с актуальными данными тренера.</param>
    /// <param name="t">Существующая сущность тренера, поля которой будут перезаписаны.</param>
    public static void UpdateEntity(this TrainersCreateUpdateDto dto, Trainer t)
    {
        t.FirstName = dto.FirstName;
        t.LastName = dto.LastName;
        t.PassportNumber = dto.PassportNumber;
        t.PhoneNumber = dto.PhoneNumber;
        t.Email = dto.Email;
        t.Gender = dto.Gender;
        t.BirthDate = dto.BirthDate;
        t.SpecializationId = dto.SpecializationId;
        t.WorkExperienceYears = dto.WorkExperienceYears;
    }
}