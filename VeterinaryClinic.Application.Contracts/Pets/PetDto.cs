using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Pets;

/// <summary>
/// Данные питомца в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="BreedId">Идентификатор породы</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Weight">Вес питомца</param>
/// <param name="OwnerId">Идентификатор владельца, если назначен</param>
public record PetDto(int Id, string Name, int BreedId, DateOnly BirthDate, double Weight, int? OwnerId);
