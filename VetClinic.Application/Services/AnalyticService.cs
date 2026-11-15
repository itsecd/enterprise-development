using Microsoft.Extensions.Logging;

using VetClinic.Application.Contracts;
using VetClinic.Application.Contracts.Appointments;
using VetClinic.Application.Contracts.Owners;
using VetClinic.Application.Contracts.Pets;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Services;

public class AnalyticService(
    IRepository<Pet> petRepository,
    IVetRepository vetRepository,
    IAppointmentRepository appointmentRepository,
    ILogger<AnalyticService> logger)
    : IAnalyticService
{
    ///<inheritdoc/>
    public async Task<List<VetDto>> GetAllVetsBySpecies(Species species)
    {
        logger.LogInformation("Аналитика: Сбор информации о ветеринарах для типа животных: {Species}.", species);
        var vets = await vetRepository.FindVetsBySpecies(species);
        logger.LogInformation("Найдено {Count} врачей со специализацией по {Species}.", vets.Count, species);
        return vets.Select(x => new VetDto(x.Id, x.FullName, x.ExpirienceYears, x.Specialization.Title)).ToList();
    }

    ///<inheritdoc/>
    public async Task<List<PetDto>> GetAllPetsByVetOrderedByName(int vetId)
    {
        logger.LogInformation("Аналитика: Сбор питомцев, записанных к врачу с Id {VetId}.", vetId);
        var appointments = await appointmentRepository.GetAll();

        var result = appointments
            .Where(a => a.VetId == vetId)
            .Select(a => a.Pet)
            .DistinctBy(p => p.Id)
            .OrderBy(p => p.Name)
            .Select(x => new PetDto(x.Id, x.Name, x.Type.ToString(), x.Breed.Title, x.Owner.FullName, x.Weight))
            .ToList();

        logger.LogInformation("Сформирован уникальный список из {Count} питомцев для врача с Id {VetId}, отсортированный по кличке.", result.Count, vetId);
        return result;
    }

    ///<inheritdoc/>
    public async Task<int> FindNumberOfSecondaryVisitForSpecificAnimalBreed(int breedId)
    {
        logger.LogInformation("Аналитика: Подсчет повторных приемов для породы с Id {BreedId}.", breedId);
        var appointments = await appointmentRepository.GetAll();

        var count = appointments.Count(a => a.Pet.BreedId == breedId && a.RepeatVisitIndicator == RepeatVisitIndicator.Yes);
        logger.LogInformation("Для породы с Id {BreedId} зафиксировано {Count} повторных визитов.", breedId, count);
        return count;
    }

    ///<inheritdoc/>
    public async Task<List<OwnerDto>> GetAllOwnersWithMultiplePets()
    {
        logger.LogInformation("Аналитика: Поиск многодетных владельцев животных (>1 питомца).");
        var pets = await petRepository.GetAll();

        var result = pets
            .GroupBy(p => p.Owner)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .OrderBy(o => o.FullName)
            .Select(x => new OwnerDto(x.Id, x.FullName, x.Phone, x.Adress))
            .ToList();

        logger.LogInformation("Выявлено {Count} владельцев, имеющих более одного питомца.", result.Count);
        return result;
    }

    ///<inheritdoc/>
    public async Task<List<AppointmentDto>> GetAllAppointmentsForCurrentMonthByRoom(int roomNumber)
    {
        var now = DateTime.Now;
        logger.LogInformation("Аналитика: Фильтрация приемов за текущий месяц ({Month}/{Year}) для кабинета №{Room}.", now.Month, now.Year, roomNumber);
        var appointments = await appointmentRepository.GetAll();

        var result = appointments
            .Where(a => a.RoomNumber == roomNumber &&
                        a.DateTime.Year == now.Year &&
                        a.DateTime.Month == now.Month)
            .Select(x => new AppointmentDto(
                x.Id, x.DateTime, x.Diagnosis, x.Pet.Name, x.Vet.FullName,
                x.Service.Title, x.Service.Price, x.RoomNumber, x.RepeatVisitIndicator.ToString()))
            .ToList();

        logger.LogInformation("В кабинете №{Room} на текущий месяц запланировано/проведено {Count} приемов.", roomNumber, result.Count);
        return result;
    }
}
