using Vet.Contracts.Dtos;
using Vet.Domain.Enums;

namespace Vet.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса аналитических запросов ветеринарной клиники
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить ветеринаров, специализирующихся на выбранном виде животного, упорядоченных по ФИО
    /// </summary>
    /// <param name="species">Целевой вид животного</param>
    /// <returns>Список DTO найденных ветеринаров</returns>
    public Task<IReadOnlyList<VeterinarianDto>> GetVeterinariansBySpeciesAsync(Species species);

    /// <summary>
    /// Получить уникальных питомцев, записанных на приём к указанному врачу, упорядоченных по кличке
    /// </summary>
    /// <param name="veterinarianId">Идентификатор ветеринара</param>
    /// <returns>Список DTO питомцев</returns>
    public Task<IReadOnlyList<PetDto>> GetPetsByVeterinarianAsync(int veterinarianId);

    /// <summary>
    /// Получить количество повторных приёмов у питомцев выбранной породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>Количество повторных приемов</returns>
    public Task<int> GetRepeatAppointmentsCountByBreedAsync(int breedId);

    /// <summary>
    /// Получить владельцев, имеющих более одного питомца, упорядоченных по ФИО
    /// </summary>
    /// <returns>Список DTO владельцев</returns>
    public Task<IReadOnlyList<OwnerDto>> GetOwnersWithMultiplePetsAsync();

    /// <summary>
    /// Получить приёмы в указанном кабинете за выбранный месяц и год, упорядоченные по времени
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц (от 1 до 12)</param>
    /// <returns>Список DTO приемов</returns>
    public Task<IReadOnlyList<AppointmentDto>> GetAppointmentsByRoomAsync(int roomNumber, int year, int month);
}