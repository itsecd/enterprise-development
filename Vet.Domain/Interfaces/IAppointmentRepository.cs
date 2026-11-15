using Vet.Domain.Entities;

namespace Vet.Domain.Interfaces;

/// <summary>
/// Репозиторий записей на приём с аналитическими выборками
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment, int>
{
    /// <summary>
    /// Асинхронно получает уникальных питомцев, записанных к указанному ветеринару, упорядоченных по кличке
    /// </summary>
    /// <param name="veterinarianId">Идентификатор ветеринара</param>
    /// <returns>Список уникальных питомцев</returns>
    public Task<IReadOnlyList<Pet>> GetDistinctPetsByVeterinarianIdAsync(int veterinarianId);

    /// <summary>
    /// Асинхронно подсчитывает количество повторных приёмов для указанной породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>Количество повторных приёмов</returns>
    public Task<int> GetRepeatCountByBreedIdAsync(int breedId);

    /// <summary>
    /// Асинхронно получает записи на приём в указанном кабинете за выбранный год и месяц, упорядоченные по времени
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц (от 1 до 12)</param>
    /// <returns>Список записей на приём</returns>
    public Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(int roomNumber, int year, int month);
}