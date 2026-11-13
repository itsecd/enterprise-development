using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Данные для создания или обновления специализации
/// </summary>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record SaveSpecializationRequest(string Name, AnimalType AnimalType);
