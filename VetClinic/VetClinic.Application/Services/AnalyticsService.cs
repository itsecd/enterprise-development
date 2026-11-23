using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Repositories;
using VetClinic.Shared;

namespace VetClinic.Application.Services;

/// <summary>
/// Аналитические запросы ветеринарной клиники, фильтрация и сортировка выполняются в базе
/// </summary>
/// <param name="owners">Репозиторий владельцев</param>
/// <param name="pets">Репозиторий питомцев</param>
/// <param name="vets">Репозиторий ветеринаров</param>
/// <param name="appointments">Репозиторий записей на прием</param>
public class AnalyticsService(
    IOwnerRepository owners,
    IPetRepository pets,
    IVetRepository vets,
    IAppointmentRepository appointments) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<VetDto>> GetVetsBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        var result = await vets.GetBySpeciesAsync(species, cancellationToken);
        return [.. result.Select(v => v.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<PetDto>> GetPetsByVetOrderedByNicknameAsync(int vetId, CancellationToken cancellationToken = default)
    {
        var result = await pets.GetByVetOrderedByNicknameAsync(vetId, cancellationToken);
        return [.. result.Select(p => p.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<int> GetRepeatAppointmentCountByBreedAsync(int breedId, CancellationToken cancellationToken = default) =>
        await appointments.CountRepeatByBreedAsync(breedId, cancellationToken);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OwnerPetsCountDto>> GetOwnersWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default)
    {
        var result = await owners.GetWithMultiplePetsOrderedByFullNameAsync(cancellationToken);
        return [.. result.Select(o => o.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAndMonthAsync(string roomNumber, int year, int month, CancellationToken cancellationToken = default)
    {
        var result = await appointments.GetByRoomAndMonthAsync(roomNumber, year, month, cancellationToken);
        return [.. result.Select(a => a.ToDto())];
    }
}
