using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Shared;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Аналитические запросы ветеринарной клиники
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Все ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    /// <param name="species">Вид животного</param>
    public Task<IReadOnlyList<VetDto>> GetVetsBySpeciesAsync(AnimalSpecies species);

    /// <summary>
    /// Все питомцы, записанные на прием к указанному врачу, упорядоченные по кличке
    /// </summary>
    /// <param name="vetId">Идентификатор врача</param>
    public Task<IReadOnlyList<PetDto>> GetPetsByVetOrderedByNicknameAsync(int vetId);

    /// <summary>
    /// Количество повторных приемов выбранной породы животного
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    public Task<int> GetRepeatAppointmentCountByBreedAsync(int breedId);

    /// <summary>
    /// Владельцы, у которых больше одного питомца, упорядоченные по ФИО, с количеством питомцев
    /// </summary>
    public Task<IReadOnlyList<OwnerPetsCountDto>> GetOwnersWithMultiplePetsOrderedByFullNameAsync();

    /// <summary>
    /// Приемы за выбранный месяц в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц от 1 до 12</param>
    public Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAndMonthAsync(string roomNumber, int year, int month);
}
