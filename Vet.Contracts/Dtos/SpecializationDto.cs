using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO специализации для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="Species">Виды животных</param>
public sealed record SpecializationDto(int Id, string Name, IReadOnlyList<Species> Species);