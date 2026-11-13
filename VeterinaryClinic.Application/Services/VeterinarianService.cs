using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «ветеринар»
/// </summary>
public sealed class VeterinarianService(IRepository<Veterinarian> veterinarians, IRepository<Specialization> specializations, IRepository<Appointment> appointments) : IVeterinarianService
{
    /// <summary>
    /// Возвращает всех ветеринаров
    /// </summary>
    public IReadOnlyList<VeterinarianDto> Veterinarians() => veterinarians.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public VeterinarianDto? Get(int id) => veterinarians.Get(id) is { } v ? DtoMapper.Map(v) : null;

    /// <summary>
    /// Создаёт ветеринара с проверкой возраста и стажа
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public VeterinarianDto Create(SaveVeterinarianRequest x)
    {
        Validate(x);
        var spec = specializations.Get(x.SpecializationId) ?? throw new ArgumentException("Specialization not found");
        return DtoMapper.Map(veterinarians.Create(id => new Veterinarian
        {
            Id = id,
            Specialization = spec,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic,
            Phone = x.Phone,
            PassportNumber = x.PassportNumber,
            BirthDate = x.BirthDate,
            ExperienceYears = x.ExperienceYears
        }));
    }

    /// <summary>
    /// Обновляет ветеринара с проверкой совместимости существующих приёмов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public VeterinarianDto Update(int id, SaveVeterinarianRequest x)
    {
        Validate(x);
        var spec = specializations.Get(x.SpecializationId) ?? throw new ArgumentException("Specialization not found");
        if (appointments.Any(a => a.Veterinarian.Id == id && a.Pet.Breed.AnimalType != spec.AnimalType))
            throw new InvalidOperationException("Specialization is incompatible with existing appointments");
        return DtoMapper.Map(veterinarians.Update(id, v =>
        {
            v.FirstName = x.FirstName;
            v.LastName = x.LastName;
            v.Patronymic = x.Patronymic;
            v.Phone = x.Phone;
            v.PassportNumber = x.PassportNumber;
            v.BirthDate = x.BirthDate;
            v.Specialization = spec;
            v.ExperienceYears = x.ExperienceYears;
        }));
    }

    /// <summary>
    /// Удаляет ветеринара без приёмов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteVeterinarian(int id)
    {
        if (appointments.Any(a => a.Veterinarian.Id == id))
            throw new InvalidOperationException("Veterinarian has appointments");
        veterinarians.Remove(id);
    }

    /// <summary>
    /// Возвращает ветеринаров по виду животных
    /// </summary>
    /// <param name="species">Вид животного</param>
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecies(AnimalType species) =>
        veterinarians.Find(v => v.Specialization.AnimalType == species).Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Возвращает приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<AppointmentDto> AppointmentsByVeterinarian(int id)
    {
        if (veterinarians.Get(id) is null) throw new KeyNotFoundException($"Veterinarian {id} not found");
        return appointments.Find(a => a.Veterinarian.Id == id).OrderBy(a => a.AppointmentDate)
            .Select(DtoMapper.Map).ToArray();
    }

    /// <summary>
    /// Возвращает питомцев ветеринара без повторений
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByVeterinarian(int id)
    {
        if (veterinarians.Get(id) is null) throw new KeyNotFoundException($"Veterinarian {id} not found");
        return appointments.Find(a => a.Veterinarian.Id == id).Select(a => a.Pet)
            .DistinctBy(p => p.Id).OrderBy(p => p.Name).Select(DtoMapper.Map).ToArray();
    }
    private static void Validate(SaveVeterinarianRequest x)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - x.BirthDate.Year;
        if (x.BirthDate > today.AddYears(-age)) age--;
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.PassportNumber) || x.BirthDate == default ||
            x.BirthDate > today || age < 18 || x.ExperienceYears < 0 || x.ExperienceYears > age - 18)
            throw new ArgumentException("Invalid veterinarian age or experience");
    }
}
