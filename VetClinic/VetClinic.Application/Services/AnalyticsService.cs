using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Shared;

namespace VetClinic.Application.Services;

/// <summary>
/// Аналитические запросы ветеринарной клиники
/// </summary>
/// <param name="owners">Репозиторий владельцев</param>
/// <param name="vets">Репозиторий ветеринаров</param>
/// <param name="appointments">Репозиторий записей на прием</param>
public class AnalyticsService(
    IOwnerRepository owners,
    IVetRepository vets,
    IAppointmentRepository appointments) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<VetDto>> GetVetsBySpeciesAsync(AnimalSpecies species)
    {
        var all = await vets.GetAllAsync();
        return [.. all
            .Where(v => v.Specialization?.FocusSpecies == species)
            .Select(v => v.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<PetDto>> GetPetsByVetOrderedByNicknameAsync(int vetId)
    {
        var all = await appointments.GetAllAsync();
        return [.. all
            .Where(a => a.Vet?.Id == vetId && a.Pet is not null)
            .Select(a => a.Pet!)
            .DistinctBy(p => p.Id)
            .OrderBy(p => p.Nickname)
            .Select(p => p.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<int> GetRepeatAppointmentCountByBreedAsync(int breedId)
    {
        var all = await appointments.GetAllAsync();
        return all.Count(a => a.IsRepeat && a.Pet?.Breed?.Id == breedId);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OwnerPetsCountDto>> GetOwnersWithMultiplePetsOrderedByFullNameAsync()
    {
        var all = await owners.GetAllAsync();
        return [.. all
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.FullName)
            .Select(o => o.ToDtoWithPetsCount())];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAndMonthAsync(string roomNumber, int year, int month)
    {
        var all = await appointments.GetAllAsync();
        return [.. all
            .Where(a => a.RoomNumber == roomNumber && a.DateTime.Year == year && a.DateTime.Month == month)
            .Select(a => a.ToDto())];
    }
}
