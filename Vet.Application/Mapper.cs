using Vet.Contracts.Dtos;
using Vet.Domain.Entities;

namespace Vet.Application;

/// <summary>
/// Методы расширения для преобразования доменных сущностей в DTO
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразование сущности породы в DTO
    /// </summary>
    /// <param name="breed">Сущность породы</param>
    /// <returns>DTO породы</returns>
    public static BreedDto ToDto(this Breed breed) =>
        new(breed.Id, breed.Species, breed.Name);

    /// <summary>
    /// Преобразование сущности специализации в DTO
    /// </summary>
    /// <param name="specialization">Сущность специализации</param>
    /// <returns>DTO специализации</returns>
    public static SpecializationDto ToDto(this Specialization specialization) =>
        new(specialization.Id, specialization.Name, [.. specialization.Species]);

    /// <summary>
    /// Преобразование сущности владельца в DTO
    /// </summary>
    /// <param name="owner">Сущность владельца</param>
    /// <returns>DTO владельца</returns>
    public static OwnerDto ToDto(this Owner owner) =>
        new(owner.Id, owner.FullName, owner.Address, owner.PhoneNumber);

    /// <summary>
    /// Преобразование сущности питомца в DTO
    /// </summary>
    /// <param name="pet">Сущность питомца</param>
    /// <returns>DTO питомца</returns>
    public static PetDto ToDto(this Pet pet) =>
        new(
            pet.Id,
            pet.Name,
            pet.Species,
            pet.Breed.ToDto(),
            pet.BirthDate,
            pet.BodyWeight,
            pet.Owner.ToDto());

    /// <summary>
    /// Преобразование сущности ветеринара в DTO
    /// </summary>
    /// <param name="veterinarian">Сущность ветеринара</param>
    /// <returns>DTO ветеринара</returns>
    public static VeterinarianDto ToDto(this Veterinarian veterinarian) =>
        new(
            veterinarian.Id,
            veterinarian.PassportNumber,
            veterinarian.FullName,
            veterinarian.Specialization.ToDto(),
            veterinarian.BirthYear,
            veterinarian.WorkExperience);

    /// <summary>
    /// Преобразование сущности записи на прием в DTO
    /// </summary>
    /// <param name="appointment">Сущность записи на прием</param>
    /// <returns>DTO записи на прием</returns>
    public static AppointmentDto ToDto(this Appointment appointment) =>
        new(
            appointment.Id,
            appointment.Pet.ToDto(),
            appointment.Veterinarian.ToDto(),
            appointment.AppointmentDateTime,
            appointment.RoomNumber,
            appointment.IsRepeat);

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность породы
    /// </summary>
    /// <param name="dto">DTO породы</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность породы</returns>
    public static Breed ToEntity(this BreedCreateUpdateDto dto, int id = 0) =>
        new()
        {
            Id = id,
            Species = dto.Species,
            Name = dto.Name
        };

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность специализации
    /// </summary>
    /// <param name="dto">DTO специализации</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность специализации</returns>
    public static Specialization ToEntity(this SpecializationCreateUpdateDto dto, int id = 0) =>
        new()
        {
            Id = id,
            Name = dto.Name,
            Species = [.. dto.Species]
        };

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность владельца
    /// </summary>
    /// <param name="dto">DTO владельца</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность владельца</returns>
    public static Owner ToEntity(this OwnerCreateUpdateDto dto, int id = 0) =>
        new()
        {
            Id = id,
            FullName = dto.FullName,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
        };

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность питомца
    /// </summary>
    /// <param name="dto">DTO питомца</param>
    /// <param name="breed">Связанная сущность породы</param>
    /// <param name="owner">Связанная сущность владельца</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность питомца</returns>
    public static Pet ToEntity(this PetCreateUpdateDto dto, Breed breed, Owner owner, int id = 0) =>
        new()
        {
            Id = id,
            Name = dto.Name,
            Species = dto.Species,
            Breed = breed,
            BirthDate = dto.BirthDate,
            BodyWeight = dto.BodyWeight,
            Owner = owner
        };

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность ветеринара
    /// </summary>
    /// <param name="dto">DTO ветеринара</param>
    /// <param name="specialization">Связанная сущность специализации</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность ветеринара</returns>
    public static Veterinarian ToEntity(this VeterinarianCreateUpdateDto dto, Specialization specialization, int id = 0) =>
        new()
        {
            Id = id,
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Specialization = specialization,
            BirthYear = dto.BirthYear,
            WorkExperience = dto.WorkExperience
        };

    /// <summary>
    /// Преобразование DTO создания/обновления в сущность записи на прием
    /// </summary>
    /// <param name="dto">DTO записи на прием</param>
    /// <param name="pet">Связанная сущность питомца</param>
    /// <param name="veterinarian">Связанная сущность ветеринара</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность записи на прием</returns>
    public static Appointment ToEntity(this AppointmentCreateUpdateDto dto, Pet pet, Veterinarian veterinarian, int id = 0) =>
        new()
        {
            Id = id,
            Pet = pet,
            Veterinarian = veterinarian,
            AppointmentDateTime = dto.AppointmentDateTime,
            RoomNumber = dto.RoomNumber,
            IsRepeat = dto.IsRepeat
        };
}