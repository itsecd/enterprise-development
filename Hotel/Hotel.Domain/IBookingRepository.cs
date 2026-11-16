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
}