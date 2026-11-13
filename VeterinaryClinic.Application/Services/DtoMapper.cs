using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Преобразует сущности клиники в DTO
/// </summary>
public static class DtoMapper
{
    /// <summary>
    /// Преобразует сущность породы в DTO
    /// </summary>
    /// <param name="x">Сущность породы для преобразования</param>
    /// <returns>DTO породы</returns>
    public static BreedDto Map(Breed x) => new(x.Id, x.Name, x.AnimalType);

    /// <summary>
    /// Преобразует сущность специализации в DTO
    /// </summary>
    /// <param name="x">Сущность специализации для преобразования</param>
    /// <returns>DTO специализации</returns>
    public static SpecializationDto Map(Specialization x) => new(x.Id, x.Name, x.AnimalType);

    /// <summary>
    /// Преобразует сущность владельца в DTO
    /// </summary>
    /// <param name="x">Сущность владельца для преобразования</param>
    /// <returns>DTO владельца</returns>
    public static OwnerDto Map(Owner x) => new(x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone, x.Address);

    /// <summary>
    /// Преобразует сущность ветеринара в DTO
    /// </summary>
    /// <param name="x">Сущность ветеринара для преобразования</param>
    /// <returns>DTO ветеринара</returns>
    public static VeterinarianDto Map(Veterinarian x) => new(x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone,
        x.PassportNumber, x.BirthDate, x.Specialization.Id, x.ExperienceYears);

    /// <summary>
    /// Преобразует сущность питомца в DTO
    /// </summary>
    /// <param name="x">Сущность питомца для преобразования</param>
    /// <returns>DTO питомца</returns>
    public static PetDto Map(Pet x) => new(x.Id, x.Name, x.Breed.Id, x.BirthDate, x.Weight, x.Owner?.Id);

    /// <summary>
    /// Преобразует сущность приёма в DTO
    /// </summary>
    /// <param name="x">Сущность приёма для преобразования</param>
    /// <returns>DTO приёма</returns>
    public static AppointmentDto Map(Appointment x) => new(x.Id, x.Pet.Id, x.Veterinarian.Id, x.AppointmentDate,
        x.RoomNumber, x.IsRepeated);
}
