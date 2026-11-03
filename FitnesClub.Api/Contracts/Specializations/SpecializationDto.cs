namespace FitnesClub.Api.Contracts.Specializations;

/// <summary>
/// Информация о специализации тренера.
/// </summary>
/// <param name="Id">Уникальный идентификатор специализации.</param>
/// <param name="Name">Наименование специализации.</param>
public record SpecializationDto(
    Guid Id,
    string Name
);