using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Contracts.Breeds;

/// <summary>
/// DTO входящих данных для создания или изменения породы животного
/// </summary>
/// <param name="Title">Название породы</param>
/// <param name="TargetSpecies">Вид животного, к которому относится порода.</param>
public record BreedCreateUpdateDto(string Title, Species TargetSpecies);
