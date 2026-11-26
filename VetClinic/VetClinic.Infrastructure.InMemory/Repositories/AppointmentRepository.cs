using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий записей на прием в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class AppointmentRepository(VetClinicSeed seed) : InMemoryRepository<Appointment>(seed, seed.Appointments), IAppointmentRepository
{
    /// <inheritdoc/>
    protected override int GetId(Appointment entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Appointment entity, int id) => entity.Id = id;

    /// <inheritdoc/>
    public Task<int> CountRepeatByBreedAsync(int breedId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Seed.Appointments.Count(appointment => appointment.IsRepeat
            && Seed.Pets.Any(pet => pet.Id == appointment.PetId && pet.BreedId == breedId)));

    /// <inheritdoc/>
    public Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(string roomNumber, int year, int month, CancellationToken cancellationToken = default)
    {
        var result = Seed.Appointments
            .Where(appointment => appointment.RoomNumber == roomNumber
                && appointment.DateTime.Year == year
                && appointment.DateTime.Month == month)
            .OrderBy(appointment => appointment.Id)
            .ToList();

        return Task.FromResult<IReadOnlyList<Appointment>>(result);
    }
}
