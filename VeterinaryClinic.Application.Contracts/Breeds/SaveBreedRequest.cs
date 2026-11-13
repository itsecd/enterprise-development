using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Данные для создания или обновления породы
/// </summary>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record SaveBreedRequest(string Name, AnimalType AnimalType);
