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
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<VetDto>> GetVetsBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    /// <summary>
    /// Все питомцы, записанные на прием к указанному врачу, упорядоченные по кличке
    /// </summary>
    /// <param name="vetId">Идентификатор врача</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<PetDto>> GetPetsByVetOrderedByNicknameAsync(int vetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Количество повторных приемов выбранной породы животного
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<int> GetRepeatAppointmentCountByBreedAsync(int breedId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Владельцы, у которых больше одного питомца, упорядоченные по ФИО, с количеством питомцев
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<OwnerPetsCountDto>> GetOwnersWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Приемы за выбранный месяц в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц от 1 до 12</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAndMonthAsync(string roomNumber, int year, int month, CancellationToken cancellationToken = default);
}
