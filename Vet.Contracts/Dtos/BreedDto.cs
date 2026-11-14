using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO породы для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Species">Вид животного</param>
/// <param name="Name">Название</param>
public sealed record BreedDto(int Id, Species Species, string Name);