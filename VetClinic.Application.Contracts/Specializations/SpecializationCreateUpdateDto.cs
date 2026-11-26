using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Contracts.Specializations;
/// <summary>
/// DTO входящих данных для создания или обновления специализации врача
/// </summary>
/// <param name="Title">Название врачебной специализации</param>
/// <param name="TargetSpecies">Вид животных, с которыми работает специалист</param>
public record SpecializationCreateUpdateDto(string Title, Species TargetSpecies);
