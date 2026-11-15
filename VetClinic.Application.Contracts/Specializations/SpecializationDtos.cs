using VetClinic.Domain.Shared.Enums;
namespace VetClinic.Application.Contracts.Specializations;

/// <summary>
/// DTO входящих данных для создания или обновления специализации врача
/// </summary>
/// <param name="Title">Название врачебной специализации</param>
/// <param name="TargetSpecies">Вид животных, с которыми работает специалист</param>
public record SpecializationCreateUpdateDto(string Title, Species TargetSpecies);

/// <summary>
/// DTO исходящих данных для отображения информации о специализации врача
/// </summary>
/// <param name="Id">Id специализации</param>
/// <param name="Title">Название врачебной специализации</param>
/// <param name="TargetSpeciesName">Строковое представление интересуемого вида животных</param>
public record SpecializationDto(int Id, string Title, string TargetSpeciesName);
