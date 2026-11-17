using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий записей на прием
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>
{
    /// <summary>
    /// Количество повторных приемов питомцев выбранной породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<int> CountRepeatByBreedAsync(int breedId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Приемы за месяц в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц от 1 до 12</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(string roomNumber, int year, int month, CancellationToken cancellationToken = default);
}
