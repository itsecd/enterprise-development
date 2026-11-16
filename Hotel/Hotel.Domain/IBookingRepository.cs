using Hotel.Domain.Entities;
using System.Linq.Expressions;

namespace Hotel.Domain;

/// <summary>
/// Репозиторий бронирований с поддержкой проверки по условию
/// </summary>
public interface IBookingRepository : IRepository<Booking, int>
{
    /// <summary>
    /// Проверить наличие бронирования, которое удовлетворяет условию
    /// </summary>
    /// <param name="predicate">Условие поиска бронирования</param>
    public Task<bool> AnyAsync(Expression<Func<Booking, bool>> predicate);

    /// <summary>
    /// Выполнить запрос к бронированиям и вернуть его результат
    /// </summary>
    /// <typeparam name="TResult">Тип элемента результата</typeparam>
    /// <param name="query">Как выбрать и обработать бронирования</param>
    public Task<IReadOnlyList<TResult>> QueryAsync<TResult>(
        Func<IQueryable<Booking>, IQueryable<TResult>> query);
}