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
    IRepository<Veterinarian, int> veterinarianRepository,
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Owner, int> ownerRepository,
    IRepository<Breed, int> breedRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<VeterinarianDto>> GetVeterinariansBySpeciesAsync(Species species)
    {
        logger.LogInformation("Запрос на получение ветеринаров по виду животного: {Species}", species);
        var vets = await veterinarianRepository.GetAllAsync();

        return [.. vets
            .Where(v => v.Specialization.Species.Contains(species))
            .OrderBy(v => v.FullName)
            .Select(v => v.ToDto())];
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

        var appointments = await appointmentRepository.GetAllAsync();

        return [.. appointments
            .Where(a => a.Veterinarian.Id == vet.Id)
            .Select(a => a.Pet)
            .Distinct()
            .OrderBy(p => p.Name)
            .Select(p => p.ToDto())];
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

        var appointments = await appointmentRepository.GetAllAsync();
        return appointments.Count(a => a.IsRepeat && a.Pet.Breed.Id == breed.Id);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OwnerDto>> GetOwnersWithMultiplePetsAsync()
    {
        logger.LogInformation("Запрос на получение владельцев с более чем одним питомцем");
        var owners = await ownerRepository.GetAllAsync();

        return [.. owners
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.FullName)
            .Select(o => o.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAsync(int roomNumber, int year, int month)
    {
        logger.LogInformation("Запрос на получение приемов в кабинете {Room} за {Month}.{Year}", roomNumber, month, year);
        var appointments = await appointmentRepository.GetAllAsync();

        return [.. appointments
            .Where(a => a.RoomNumber == roomNumber
                     && a.AppointmentDateTime.Year == year
                     && a.AppointmentDateTime.Month == month)
            .OrderBy(a => a.AppointmentDateTime)
            .Select(a => a.ToDto())];
    }
}