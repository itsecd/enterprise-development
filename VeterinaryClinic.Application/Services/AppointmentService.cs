using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «приём»
/// </summary>
public sealed class AppointmentService(IRepository<Appointment> appointments, IRepository<Pet> pets, IRepository<Veterinarian> veterinarians) : IAppointmentService
{
    /// <summary>
    /// Возвращает все приёмы
    /// </summary>
    public IReadOnlyList<AppointmentDto> Appointments() => appointments.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Возвращает приёмы за указанный месяц и кабинет
    /// </summary>
    /// <param name="room">Номер кабинета</param>
    /// <param name="today">Дата, относительно которой выполняется выборка</param>
    public IReadOnlyList<AppointmentDto> AppointmentsThisMonth(int room, DateTime today) =>
        appointments.Find(a => a.RoomNumber == room && a.AppointmentDate.Year == today.Year &&
            a.AppointmentDate.Month == today.Month).OrderBy(a => a.AppointmentDate).Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public AppointmentDto? Get(int id) => appointments.Get(id) is { } a ? DtoMapper.Map(a) : null;

    /// <summary>
    /// Создаёт приём после проверки данных и занятости кабинета
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public AppointmentDto Create(SaveAppointmentRequest x)
    {
        Validate(x);
        var pet = pets.Get(x.PetId) ?? throw new ArgumentException("Pet not found");
        var vet = veterinarians.Get(x.VeterinarianId) ?? throw new ArgumentException("Veterinarian not found");
        EnsureCompatible(pet, vet);
        EnsureAvailable(x, null);
        var saved = appointments.Create(id => new Appointment
        {
            Id = id,
            Pet = pet,
            Veterinarian = vet,
            RoomNumber = x.RoomNumber,
            AppointmentDate = x.AppointmentDate,
            IsRepeated = x.IsRepeated
        });
        return DtoMapper.Map(saved);
    }

    /// <summary>
    /// Обновляет приём, исключая его из проверки конфликтов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public AppointmentDto Update(int id, SaveAppointmentRequest x)
    {
        Validate(x);
        if (appointments.Get(id) is null) throw new KeyNotFoundException($"Appointment {id} not found");
        var pet = pets.Get(x.PetId) ?? throw new ArgumentException("Pet not found");
        var vet = veterinarians.Get(x.VeterinarianId) ?? throw new ArgumentException("Veterinarian not found");
        EnsureCompatible(pet, vet);
        EnsureAvailable(x, id);
        return DtoMapper.Map(appointments.Update(id, a =>
        {
            a.Pet = pet;
            a.Veterinarian = vet;
            a.RoomNumber = x.RoomNumber;
            a.AppointmentDate = x.AppointmentDate;
            a.IsRepeated = x.IsRepeated;
        }));
    }

    /// <summary>
    /// Удаляет приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteAppointment(int id) => appointments.Remove(id);

    private static void Validate(SaveAppointmentRequest x)
    {
        if (x.RoomNumber <= 0 || x.AppointmentDate < DateTime.Today || x.AppointmentDate > DateTime.Today.AddYears(5))
            throw new ArgumentException("Appointment date must be today or within the next five years");
    }

    private static void EnsureCompatible(Pet pet, Veterinarian vet)
    {
        if (pet.Breed.AnimalType != vet.Specialization.AnimalType)
            throw new ArgumentException("Veterinarian specialization does not match pet species");
    }

    private void EnsureAvailable(SaveAppointmentRequest x, int? excludedId)
    {
        if (appointments.Any(a => a.Id != excludedId && a.RoomNumber == x.RoomNumber &&
            a.AppointmentDate == x.AppointmentDate))
            throw new InvalidOperationException("Room is already booked for this time");
    }
}
