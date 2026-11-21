using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Interfaces;

/// <summary>Аналитические запросы, выполняемые хранилищем.</summary>
public interface IAnalyticsRepository
{
    /// <summary>Возвращает тренеров со стажем не менее пяти лет.</summary>
    public Task<IReadOnlyCollection<Trainer>> GetExperiencedTrainersAsync();

    /// <summary>Проверяет занятость зала в указанный момент UTC.</summary>
    /// <param name="hallId">Идентификатор зала.</param>
    /// <param name="now">Проверяемый момент UTC.</param>
    public Task<bool> IsHallOccupiedAsync(int hallId, DateTime now);

    /// <summary>Возвращает клиентов с абонементами, истёкшими до указанной даты.</summary>
    /// <param name="today">Дата проверки.</param>
    public Task<IReadOnlyCollection<Client>> GetExpiredClientsAsync(DateTime today);

    /// <summary>Возвращает тренировки зала в интервале от start включительно до end исключительно.</summary>
    /// <param name="hallId">Идентификатор зала.</param>
    /// <param name="start">Начало интервала UTC.</param>
    /// <param name="end">Конец интервала UTC.</param>
    public Task<IReadOnlyCollection<Training>> GetHallTrainingsAsync(int hallId, DateTime start, DateTime end);

    /// <summary>Возвращает до пяти тренеров с наибольшим количеством тренировок.</summary>
    public Task<IReadOnlyCollection<Trainer>> GetTopTrainersAsync();
}
