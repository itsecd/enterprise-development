using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Данные для создания или обновления породы
/// </summary>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record SaveBreedRequest(string Name, AnimalType AnimalType);
