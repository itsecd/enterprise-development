namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления специализации тренера
/// </summary>
/// <param name="Id">Уникальный идентификатор специализации</param>
/// <param name="Name">Название специализации</param>
public record SpecializationDto(int Id, string Name);