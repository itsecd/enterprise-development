using Microsoft.Extensions.Logging;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
using Vet.Domain.Entities;
using Vet.Domain.Enums;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
/// <param name="appointmentRepository">Репозиторий приемов</param>
/// <param name="ownerRepository">Репозиторий владельцев</param>
/// <param name="breedRepository">Репозиторий пород</param>
/// <param name="logger">Логгер сервиса</param>
public class AnalyticsService(
    IVeterinarianRepository veterinarianRepository,
    IAppointmentRepository appointmentRepository,
    IOwnerRepository ownerRepository,
    IRepository<Breed, int> breedRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<VeterinarianDto>> GetVeterinariansBySpeciesAsync(Species species)
    {
        logger.LogInformation("Запрос на получение ветеринаров по виду животного: {Species}", species);
        var vets = await veterinarianRepository.GetBySpeciesAsync(species);
        return [.. vets.Select(v => v.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<PetDto>> GetPetsByVeterinarianAsync(int veterinarianId)
    {
        logger.LogInformation("Запрос на получение питомцев ветеринара с Id {VetId}", veterinarianId);
        var vet = await veterinarianRepository.GetByIdAsync(veterinarianId);
        if (vet is null)
        {
            logger.LogWarning("Ветеринар с идентификатором {VetId} не найден в клинике", veterinarianId);
            throw new KeyNotFoundException($"Ветеринар с идентификатором {veterinarianId} не найден в клинике");
        }

        var pets = await appointmentRepository.GetDistinctPetsByVeterinarianIdAsync(vet.Id);
        return [.. pets.Select(p => p.ToDto())];
    }

    /// <inheritdoc />
    public async Task<int> GetRepeatAppointmentsCountByBreedAsync(int breedId)
    {
        logger.LogInformation("Запрос на подсчет повторных приемов для породы с Id {BreedId}", breedId);
        var breed = await breedRepository.GetByIdAsync(breedId);
        if (breed is null)
        {
            logger.LogWarning("Порода с идентификатором {BreedId} не найдена в справочнике", breedId);
            throw new KeyNotFoundException($"Порода с идентификатором {breedId} не найдена в справочнике");
        }

        return await appointmentRepository.GetRepeatCountByBreedIdAsync(breed.Id);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OwnerDto>> GetOwnersWithMultiplePetsAsync()
    {
        logger.LogInformation("Запрос на получение владельцев с более чем одним питомцем");
        var owners = await ownerRepository.GetWithMultiplePetsAsync();
        return [.. owners.Select(o => o.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAsync(int roomNumber, int year, int month)
    {
        logger.LogInformation("Запрос на получение приемов в кабинете {Room} за {Month}.{Year}", roomNumber, month, year);
        var appointments = await appointmentRepository.GetByRoomAndMonthAsync(roomNumber, year, month);
        return [.. appointments.Select(a => a.ToDto())];
    }
}