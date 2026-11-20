using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Данные специализации в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="AnimalType">Вид животного</param>
public record SpecializationDto(int Id, string Name, AnimalType AnimalType);
