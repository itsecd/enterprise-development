using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Данные специализации в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record SpecializationDto(int Id, string Name, AnimalType AnimalType);
