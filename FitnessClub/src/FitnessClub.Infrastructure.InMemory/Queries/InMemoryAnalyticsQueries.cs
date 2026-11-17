using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;


namespace FitnessClub.Infrastructure.InMemory.Queries;
public class InMemoryAnalyticsQueries(
    IRepository<Trainer> trainerRepository,
    IRepository<Client> clientRepository,
    IRepository<Booking> bookingRepository) : IAnalyticsQueries
{
    /// <summary>
    /// Возвращает тренеров, чей стаж не меньше заданного
    /// </summary>
    /// <param name="minimumExperience">Минимальный стаж в годах</param>
    /// <returns>Список тренеров, удовлетворяющих условию</returns>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience,
        CancellationToken cancellationToken)
    {
        var trainers = await trainerRepository.GetAllAsync(cancellationToken);
        return [.. trainers
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
            ))];
    }

    /// <summary>
    /// Возвращает клиентов с просроченной подпиской, отсортированных по ФИО
    /// </summary>
    /// <param name="asOfDate">Дата, относительно которой проверяется срок подписки</param>
    /// <returns>Список клиентов, у которых подписка закончилась до указанной даты</returns>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate,
        CancellationToken cancellationToken)
    {
        var clients = await clientRepository.GetAllAsync(cancellationToken);
        return [.. clients
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
            ))];
    }

    /// <summary>
    /// Возвращает тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count">Максимальное количество тренеров в результате</param>
    /// <returns>Тренеры, отсортированные по числу бронирований по убыванию, затем по идентификатору</returns>
    /// <exception cref="ArgumentOutOfRangeException"
    /// ><paramref name="count"/> меньше или равен нулю
    /// </exception>
    public async Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count,
        CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        var bookings = await bookingRepository.GetAllAsync(cancellationToken);
        return [.. bookings
            .GroupBy(booking => booking.Trainer.Id)
            .Select(group => new PopularTrainerDto(
                group.Key,
                group.First().Trainer.FullName,
                group.Count()))
            .OrderByDescending(trainer => trainer.BookingCount)
            .ThenBy(trainer => trainer.TrainerId)
            .Take(count)];
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
        var bookings = await bookingRepository.GetAllAsync(cancellationToken);
        return !bookings.Any(booking =>
            booking.Hall.Id == hallId &&
            booking.DateTime <= dateTime &&
            dateTime < booking.DateTime + booking.Duration);
    }

    /// <summary>
    /// Возвращает бронирования выбранного зала за указанный месяц
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateInMonth">Дата, определяющая месяц и год поиска</param>
    /// <returns>Список бронирований зала за указанный месяц</returns>
    /// >Зал с указанным идентификатором не найден
    /// </exception>
    public async Task<IReadOnlyList<BookingDto>> GetHallBookingsForMonthAsync(
        int hallId,
        DateOnly dateInMonth,
        CancellationToken cancellationToken)
    {
        var bookings = await bookingRepository.GetAllAsync(cancellationToken);
        return [.. bookings
            .Where(booking =>
                booking.Hall.Id == hallId &&
                booking.DateTime.Year == dateInMonth.Year &&
                booking.DateTime.Month == dateInMonth.Month)
            .Select(booking => new BookingDto(
                booking.Id,
                booking.Client.Id,
                booking.Trainer.Id,
                booking.Hall.Id,
                booking.DateTime,
                booking.IsTrial,
                booking.Duration
            ))
            .OrderBy(booking => booking.DateTime)
            .ThenBy(booking => booking.Id)];
    }
}
