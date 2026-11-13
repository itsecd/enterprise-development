using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Данные породы в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record BreedDto(int Id, string Name, AnimalType AnimalType);
