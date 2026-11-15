using VetClinic.Domain.Shared.Enums;
namespace VetClinic.Application.Contracts.Breeds;
/// <summary>
/// DTO входящих данных для создания или изменения породы животного
/// </summary>
/// <param name="Title">Название породы</param>
/// <param name="TargetSpecies">Вид животного, к которому относится порода.</param>
public record BreedCreateUpdateDto(string Title, Species TargetSpecies);

/// <summary>
/// DTO исходящих данных для отображения информации о породе животного
/// </summary>
/// <param name="Id">Id породы</param>
/// <param name="Title">Название породы</param>
/// <param name="TargetSpeciesName">Строковое представление вида животного</param>
public record BreedDto(int Id, string Title, string TargetSpeciesName);
