using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «приём»
/// </summary>
public sealed class AppointmentService(IRepository<Appointment> appointments, IRepository<Pet> pets, IRepository<Veterinarian> veterinarians) : IAppointmentService
{
    private static void Validate(SaveAppointmentRequest x)
    {
        if (x.RoomNumber <= 0 || x.AppointmentDate < DateTime.Today || x.AppointmentDate > DateTime.Today.AddYears(5))
            throw new ArgumentException("Дата приёма должна быть не раньше сегодняшнего дня и не позднее пяти лет");
    }

    private static void EnsureCompatible(Pet pet, Veterinarian vet)
    {
        if (pet.Breed.AnimalType != vet.Specialization.AnimalType)
            throw new ArgumentException("Специализация ветеринара не соответствует виду питомца");
    }

    public async Task<IReadOnlyList<AppointmentDto>> AppointmentsAsync(CancellationToken cancellationToken = default) => (await appointments.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<IReadOnlyList<AppointmentDto>> AppointmentsThisMonthAsync(int room, DateTime today, CancellationToken cancellationToken = default) => (await appointments.FindAsync(a => a.RoomNumber == room && a.AppointmentDate.Year == today.Year &&
            a.AppointmentDate.Month == today.Month, cancellationToken)).OrderBy(a => a.AppointmentDate).Select(DtoMapper.Map).ToArray();

    public async Task<AppointmentDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await appointments.GetAsync(id, cancellationToken)) is { } a ? DtoMapper.Map(a) : null;

    public async Task<AppointmentDto> CreateAsync(SaveAppointmentRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var pet = (await pets.GetAsync(x.PetId, cancellationToken)) ?? throw new ArgumentException("Питомец не найден");
        var vet = (await veterinarians.GetAsync(x.VeterinarianId, cancellationToken)) ?? throw new ArgumentException("Ветеринар не найден");
        EnsureCompatible(pet, vet);
        await EnsureAvailableAsync(x, null, cancellationToken);
        var saved = (await appointments.CreateAsync(new Appointment
        {
            Pet = pet,
            Veterinarian = vet,
            RoomNumber = x.RoomNumber,
            AppointmentDate = x.AppointmentDate,
            IsRepeated = x.IsRepeated
        }, cancellationToken));
        return DtoMapper.Map(saved);
    }


    public async Task<AppointmentDto> UpdateAsync(int id, SaveAppointmentRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        if ((await appointments.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Приём {id} не найден");
        var pet = (await pets.GetAsync(x.PetId, cancellationToken)) ?? throw new ArgumentException("Питомец не найден");
        var vet = (await veterinarians.GetAsync(x.VeterinarianId, cancellationToken)) ?? throw new ArgumentException("Ветеринар не найден");
        EnsureCompatible(pet, vet);
        await EnsureAvailableAsync(x, id, cancellationToken);
        return DtoMapper.Map((await appointments.UpdateAsync(id, a =>
        {
            a.Pet = pet;
            a.Veterinarian = vet;
            a.RoomNumber = x.RoomNumber;
            a.AppointmentDate = x.AppointmentDate;
            a.IsRepeated = x.IsRepeated;
        }, cancellationToken)));
    }


    public async Task DeleteAppointmentAsync(int id, CancellationToken cancellationToken = default) { await appointments.RemoveAsync(id, cancellationToken); }

    private async Task EnsureAvailableAsync(SaveAppointmentRequest x, int? excludedId, CancellationToken cancellationToken)
    {
        if (await appointments.AnyAsync(a => a.Id != excludedId && a.RoomNumber == x.RoomNumber &&
            a.AppointmentDate == x.AppointmentDate, cancellationToken))
            throw new InvalidOperationException("Кабинет уже занят на это время");
    }
}
