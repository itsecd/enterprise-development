using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Реализация сервиса аналитических запросов к данным ветеринарной клиники
/// </summary>
public class VetClinicService(
    IRepository<Veterinarian> veterinarianRepository,
    IRepository<Appointment> appointmentRepository,
    IRepository<Owner> ownerRepository) : IVetClinicService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Veterinarian>> GetVeterinariansBySpeciesAsync(AnimalSpecies species)
    {
        var vets = await veterinarianRepository.GetAllAsync();
        return vets.Where(v => v.Specialization?.TargetSpecies == species);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Pet>> GetPetsByDoctorOrderedByNameAsync(int doctorId)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        return appointments
            .Where(a => a.VeterinarianId == doctorId && a.Pet is not null)
            .Select(a => a.Pet!)
            .DistinctBy(p => p.Id)
            .OrderBy(p => p.Name);
    }

    /// <inheritdoc />
    public async Task<int> GetFollowUpAppointmentsCountByBreedAsync(int breedId)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        return appointments
            .Count(a => a.Pet?.BreedId == breedId && a.IsFollowUp);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Owner>> GetOwnersWithMultiplePetsOrderedByNameAsync()
    {
        var owners = await ownerRepository.GetAllAsync();
        return owners
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.FullName);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Appointment>> GetCurrentMonthAppointmentsByRoomAsync(int roomNumber, DateTime currentDate)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        return appointments
            .Where(a => a.RoomNumber == roomNumber
                        && a.DateTime.Year == currentDate.Year
                        && a.DateTime.Month == currentDate.Month);
    }
}