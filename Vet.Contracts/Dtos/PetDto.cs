using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO питомца для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Кличка</param>
/// <param name="Species">Вид животного</param>
/// <param name="Breed">DTO породы животного</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="BodyWeight">Масса тела</param>
/// <param name="Owner">DTO владельца</param>
public sealed record PetDto(int Id, string Name, Species Species, BreedDto Breed, DateOnly? BirthDate, double? BodyWeight, OwnerDto Owner);