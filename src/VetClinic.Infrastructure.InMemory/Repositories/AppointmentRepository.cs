using VetClinic.Domain.Entities;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Rules;

namespace VetClinic.Infrastructure.InMemory.Repositories;

public sealed class AppointmentRepository(InMemoryClinicStore store) : IAppointmentRepository
{
    public Task<IReadOnlyList<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Appointment>>([.. store.Appointments.OrderBy(appointment => appointment.VisitDateTime)]);
        }
    }

    public Task<Appointment?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult(store.Appointments.FirstOrDefault(appointment => appointment.Id == id));
        }
    }

    public Task<IReadOnlyList<Appointment>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            _ = store.Veterinarians.FirstOrDefault(veterinarian => veterinarian.Id == veterinarianId)
                ?? throw new EntityNotFoundException("ветеринар", veterinarianId);

            return Task.FromResult<IReadOnlyList<Appointment>>([.. store.Appointments
                .Where(appointment => appointment.Veterinarian.Id == veterinarianId)
                .OrderBy(appointment => appointment.VisitDateTime)]);
        }
    }

    public Task<IReadOnlyList<Appointment>> GetByPetAsync(int petId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            _ = store.Pets.FirstOrDefault(pet => pet.Id == petId)
                ?? throw new EntityNotFoundException("питомец", petId);

            return Task.FromResult<IReadOnlyList<Appointment>>([.. store.Appointments
                .Where(appointment => appointment.Pet.Id == petId)
                .OrderBy(appointment => appointment.VisitDateTime)]);
        }
    }

    public Task<IReadOnlyList<Appointment>> GetByRoomAsync(string roomNumber, DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Appointment>>([.. store.Appointments
                .Where(appointment => appointment.RoomNumber == roomNumber
                    && appointment.VisitDateTime >= from
                    && appointment.VisitDateTime < to)
                .OrderBy(appointment => appointment.VisitDateTime)]);
        }
    }

    public Task<int> CountFollowUpsAsync(int breedId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            _ = store.Breeds.FirstOrDefault(breed => breed.Id == breedId)
                ?? throw new EntityNotFoundException("порода", breedId);

            return Task.FromResult(store.Appointments.Count(appointment =>
                appointment.IsFollowUp && appointment.Pet.Breed.Id == breedId));
        }
    }

    public Task<Appointment> AddAsync(
        int petId,
        int veterinarianId,
        DateTime visitDateTime,
        string roomNumber,
        bool isFollowUp,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var pet = FindPet(petId);
            var veterinarian = FindVeterinarian(veterinarianId);
            SpeciesMatch.Ensure(pet, veterinarian);

            var appointment = new Appointment
            {
                Id = store.TakeAppointmentId(),
                Pet = pet,
                Veterinarian = veterinarian,
                VisitDateTime = visitDateTime,
                RoomNumber = roomNumber,
                IsFollowUp = isFollowUp
            };
            store.Appointments.Add(appointment);
            return Task.FromResult(appointment);
        }
    }

    public Task<Appointment> UpdateAsync(
        int id,
        int petId,
        int veterinarianId,
        DateTime visitDateTime,
        string roomNumber,
        bool isFollowUp,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var appointment = store.Appointments.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("запись на приём", id);
            var pet = FindPet(petId);
            var veterinarian = FindVeterinarian(veterinarianId);
            SpeciesMatch.Ensure(pet, veterinarian);

            appointment.Pet = pet;
            appointment.Veterinarian = veterinarian;
            appointment.VisitDateTime = visitDateTime;
            appointment.RoomNumber = roomNumber;
            appointment.IsFollowUp = isFollowUp;
            return Task.FromResult(appointment);
        }
    }

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var appointment = store.Appointments.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("запись на приём", id);
            store.Appointments.Remove(appointment);
        }

        return Task.CompletedTask;
    }

    private Pet FindPet(int petId) =>
        store.Pets.FirstOrDefault(pet => pet.Id == petId)
        ?? throw new EntityNotFoundException("питомец", petId);

    private Veterinarian FindVeterinarian(int veterinarianId) =>
        store.Veterinarians.FirstOrDefault(veterinarian => veterinarian.Id == veterinarianId)
        ?? throw new EntityNotFoundException("ветеринар", veterinarianId);
}
