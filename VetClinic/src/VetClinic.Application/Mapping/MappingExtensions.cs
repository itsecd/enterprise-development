using VetClinic.Application.Dto;
using VetClinic.Domain.Entities;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Методы расширения для преобразования сущностей в объекты передачи данных
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразование сущности породы в модель ответа
    /// </summary>
    public static BreedDto ToDto(this Breed breed)
    {
        return new BreedDto
        {
            Id = breed.Id,
            Name = breed.Name,
            Species = breed.Species
        };
    }

    /// <summary>
    /// Преобразование сущности специализации в модель ответа
    /// </summary>
    public static SpecializationDto ToDto(this Specialization specialization)
    {
        return new SpecializationDto
        {
            Id = specialization.Id,
            Name = specialization.Name,
            TargetSpecies = specialization.TargetSpecies
        };
    }

    /// <summary>
    /// Преобразование сущности владельца в модель ответа
    /// </summary>
    public static OwnerDto ToDto(this Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            FullName = owner.FullName,
            Phone = owner.Phone,
            Address = owner.Address,
            PetsCount = owner.Pets.Count
        };
    }

    /// <summary>
    /// Преобразование сущности питомца в модель ответа
    /// </summary>
    public static PetDto ToDto(this Pet pet)
    {
        return new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            BreedId = pet.BreedId,
            BreedName = pet.Breed?.Name,
            BirthDate = pet.BirthDate,
            WeightKg = pet.WeightKg,
            OwnerId = pet.OwnerId,
            OwnerName = pet.Owner?.FullName
        };
    }

    /// <summary>
    /// Преобразование сущности врача в модель ответа
    /// </summary>
    public static VeterinarianDto ToDto(this Veterinarian veterinarian)
    {
        return new VeterinarianDto
        {
            Id = veterinarian.Id,
            PassportNumber = veterinarian.PassportNumber,
            FullName = veterinarian.FullName,
            BirthYear = veterinarian.BirthYear,
            ExperienceYears = veterinarian.ExperienceYears,
            SpecializationId = veterinarian.SpecializationId,
            SpecializationName = veterinarian.Specialization?.Name
        };
    }

    /// <summary>
    /// Преобразование сущности приема в модель ответа
    /// </summary>
    public static AppointmentDto ToDto(this Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            DateTime = appointment.DateTime,
            RoomNumber = appointment.RoomNumber,
            IsFollowUp = appointment.IsFollowUp,
            PetId = appointment.PetId,
            PetName = appointment.Pet?.Name,
            VeterinarianId = appointment.VeterinarianId,
            VeterinarianName = appointment.Veterinarian?.FullName
        };
    }
}