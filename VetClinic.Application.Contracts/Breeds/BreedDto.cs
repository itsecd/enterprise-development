namespace VetClinic.Application.Contracts.Breeds;


/// <summary>
/// DTO исходящих данных для отображения информации о породе животного
/// </summary>
/// <param name="Id">Id породы</param>
/// <param name="Title">Название породы</param>
/// <param name="TargetSpeciesName">Строковое представление вида животного</param>
public record BreedDto(int Id, string Title, string TargetSpeciesName);
