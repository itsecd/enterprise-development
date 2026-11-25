using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Queries;

/// <summary>
/// Выполняет аналитические запросы к базе данных с помощью EFCore
/// </summary>
public class EfCoreAnalyticsQueries(FitnessClubDbContext dbContext) : IAnalyticsQueries
{
    /// <summary>
    /// Возвращает тренеров, стаж которых не меньше заданного
    /// </summary>
    /// <param name="minimumExperience">Минимальный стаж в годах</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Тренеры, удовлетворяющие условию, в порядке возрастания идентификатора</returns>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience,
        CancellationToken cancellationToken)
    {
        return await dbContext.Trainers
            .AsNoTracking()
            .Where(trainer => trainer.Experience >= minimumExperience)
            .OrderBy(trainer => trainer.Id)
            .Select(trainer => new TrainerDto(
                trainer.Id,
                trainer.FullName,
                trainer.PassportNumber,
                trainer.Gender,
                trainer.BirthDate,
                trainer.PhoneNumber,
                trainer.Specialization.Id,
                trainer.Specialization.Name,
                trainer.Experience
            ))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Возвращает бронирования указанного зала за календарный месяц, заданный датой
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateInMonth">Дата, определяющая месяц и год выборки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Бронирования зала за указанный месяц, отсортированные по дате и идентификатору</returns>
    public async Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(int hallId, DateOnly dateInMonth, CancellationToken cancellationToken)
    {
        var start = new DateTime(dateInMonth.Year, dateInMonth.Month, 1);
        var end = start.AddMonths(1);

        return await dbContext.Bookings
            .AsNoTracking()
            .Where(booking =>
                booking.Hall.Id == hallId &&
                booking.DateTime >= start &&
                booking.DateTime < end)
            .OrderBy(booking => booking.DateTime)
            .ThenBy(booking => booking.Id)
            .Select(booking => new BookingDto(
                booking.Id,
                booking.Client.Id,
                booking.Trainer.Id,
                booking.Hall.Id,
                booking.DateTime,
                booking.IsTrial,
                booking.Duration
            ))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Возвращает клиентов, срок действия подписки которых истёк до указанной даты
    /// </summary>
    /// <param name="asOfDate">Дата, относительно которой проверяется срок действия подписки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Клиенты с просроченными подписками, отсортированные по имени и идентификатору</returns>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Clients
            .AsNoTracking()
            .Where(client => client.SubscriptionEnd < asOfDate)
            .OrderBy(client => client.FullName)
            .ThenBy(client => client.Id)
            .Select(client => new ClientDto(
                client.Id,
                client.FullName,
                client.PassportNumber,
                client.Gender,
                client.BirthDate,
                client.PhoneNumber,
                client.SubscriptionStart,
                client.SubscriptionEnd
            ))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Возвращает тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count">Максимальное количество тренеров в результате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Тренеры, отсортированные по числу бронирований по убыванию, затем по идентификатору</returns>
    public async Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count,
        CancellationToken cancellationToken)
    {
        var bookingCounts = dbContext.Bookings
            .AsNoTracking()
            .GroupBy(booking => EF.Property<int>(booking, "TrainerId"))
            .Select(group => new
            {
                TrainerId = group.Key,
                BookingCount = group.Count()
            });

        return await bookingCounts
            .Join(
                dbContext.Trainers.AsNoTracking(),
                counts => counts.TrainerId,
                trainer => trainer.Id,
                (counts, trainer) => new
                {
                    counts.BookingCount,
                    Trainer = trainer
                })
            .OrderByDescending(result => result.BookingCount)
            .ThenBy(result => result.Trainer.Id)
            .Take(count)
            .Select(result => new PopularTrainerDto(
                result.Trainer.Id,
                result.Trainer.FullName,
                result.BookingCount))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Проверяет, нет ли бронирования зала, интервал которого включает указанное время
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateTime">Дата и время проверки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    /// <see langword="true"/>, если на указанное время нет бронирования;
    /// в противном случае — <see langword="false"/>
    /// </returns>
    public async Task<bool> IsHallAvailableAsync(int hallId, DateTime dateTime, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .Where(booking =>
                booking.Hall.Id == hallId &&
                booking.DateTime <= dateTime &&
                dateTime < booking.DateTime + booking.Duration)
            .AnyAsync(cancellationToken) == false;
    }
}