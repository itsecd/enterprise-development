namespace VetClinic.Application.Contracts.Specializations;



/// <summary>
/// DTO исходящих данных для отображения информации о специализации врача
/// </summary>
/// <param name="Id">Id специализации</param>
/// <param name="Title">Название врачебной специализации</param>
/// <param name="TargetSpeciesName">Строковое представление интересуемого вида животных</param>
public record SpecializationDto(int Id, string Title, string TargetSpeciesName);
