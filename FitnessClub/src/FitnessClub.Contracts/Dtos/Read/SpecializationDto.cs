using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Contracts.Dtos.Read;
/// <summary>
/// Данные специализации для ответа API
/// </summary>
/// <param name="Id">
/// Идентификатор специализации
/// </param>
/// <param name="Name">
/// Название специализации
/// </param>
public record SpecializationDto
(
    int Id,
    string Name
) : IHasId;