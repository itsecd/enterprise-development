using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Contracts.Pets;

/// <summary>
/// DTO входящих данных для создания записи или изменения уже существующей записи о питомце 
/// </summary>
/// <param name="Name">Кличка</param>
/// <param name="Type">Вид животного</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Weight">Вес животного</param>
/// <param name="BreedId">Id породы</param>
/// <param name="OwnerId">Id владельца</param>
public record PetCreateUpdateDto(string Name, Species Type, DateOnly BirthDate, decimal Weight, int BreedId, int OwnerId);

