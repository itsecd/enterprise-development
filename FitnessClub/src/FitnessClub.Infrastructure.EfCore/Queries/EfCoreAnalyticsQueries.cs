using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Queries;

public class EfCoreAnalyticsQueries(FitnessClubDbContext dbContext) : IAnalyticsQueries
{
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

    public async Task<IReadOnlyList<BookingDto>> GetHallBookingsForMonthAsync(int hallId, DateOnly dateInMonth, CancellationToken cancellationToken)
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


    public async Task<IReadOnlyList<PopularTrainerDto>>GetMostPopularTrainersAsync(
        int count,
        CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .GroupBy(booking => new
            {
                booking.Trainer.Id,
                booking.Trainer.FullName
            })
            .Select(group => new PopularTrainerDto(
                group.Key.Id,
                group.Key.FullName,
                group.Count()))
            .OrderByDescending(trainer => trainer.BookingCount)
            .ThenBy(trainer => trainer.TrainerId)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Проверяет, свободен ли зал в указанное время ищет бронирование этого зала,
    /// интервал которого включает указанное время
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateTime">Дата и время, для которого проверяется доступность</param>
    /// <returns><see langword="true"/>, если на указанное время нет бронирования, иначе <see langword="false"/></returns>
    /// >Зал с указанным идентификатором не найден
    /// </exception>
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