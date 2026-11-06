using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services.Analytics;

public class AnalyticsService(
    IRepository<Trainer> trainerRepository,
    IRepository<Client> clientRepository,
    IRepository<Booking> bookingRepository,
    IRepository<Hall> hallRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <summary>
    /// Возвращает тренеров, чей стаж не меньше заданного
    /// </summary>
    /// <param name="minimumExperience"
    /// >Минимальный стаж в годах
    /// </param>
    /// <returns>
    /// Список тренеров, удовлетворяющих условию
    /// </returns>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience = 5)
    {
        logger.LogDebug("Поиск тренеров со стажем от {MinimumExperience} лет", minimumExperience);
        var trainers = await trainerRepository.GetAllAsync();
        return [.. trainers
            .Where(trainer => trainer.Experience >= minimumExperience)
            .Select(trainer => trainer.ToDto())];
    }

    /// <summary>
    /// Возвращает клиентов с просроченной подпиской, отсортированных по ФИО
    /// </summary>
    /// <param name="asOfDate"
    /// >Дата, относительно которой проверяется срок подписки
    /// </param>
    /// <returns>
    /// Список клиентов, у которых подписка закончилась до указанной даты
    /// </returns>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate)
    {
        logger.LogDebug("Поиск клиентов с просроченной подпиской на дату {AsOfDate}", asOfDate);
        var clients = await clientRepository.GetAllAsync();
        return [.. clients
            .Where(client => client.SubscriptionEnd < asOfDate)
            .OrderBy(client => client.FullName)
            .Select(client => client.ToDto())];
    }

    /// <summary>
    /// Возвращает тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count"
    /// >Максимальное количество тренеров в результате
    /// </param>
    /// <returns>
    /// Тренеры, отсортированные по числу бронирований по убыванию, затем по идентификатору
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"
    /// ><paramref name="count"/> меньше или равен нулю
    /// </exception>
    public async Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count = 5)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        logger.LogDebug("Поиск {Count} самых популярных тренеров", count);
        var bookings = await bookingRepository.GetAllAsync();
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
    /// Проверяет, свободен ли зал в указанное время
    /// Сначала проверяет существование зала, затем ищет бронирование этого зала,
    /// интервал которого включает указанное время
    /// </summary>
    /// <param name="hallId"
    /// >Идентификатор зала
    /// </param>
    /// <param name="dateTime"
    /// >Дата и время, для которого проверяется доступность
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если на указанное время нет бронирования,
    /// иначе <see langword="false"/>
    /// </returns>
    /// <exception cref="NotFoundException"
    /// >Зал с указанным идентификатором не найден
    /// </exception>
    public async Task<bool> IsHallAvailableAsync(int hallId, DateTime dateTime)
    {
        logger.LogDebug("Проверка доступности зала {HallId} на {DateTime}", hallId, dateTime);
        await EnsureHallExistsAsync(hallId);

        var bookings = await bookingRepository.GetAllAsync();
        return !bookings.Any(booking =>
            booking.Hall.Id == hallId &&
            booking.DateTime <= dateTime &&
            dateTime < booking.DateTime + booking.Duration);
    }

    /// <summary>
    /// Возвращает бронирования выбранного зала за указанный месяц
    /// </summary>
    /// <param name="hallId"
    /// >Идентификатор зала
    /// </param>
    /// <param name="dateInMonth"
    /// >Дата, определяющая месяц и год поиска
    /// </param>
    /// <returns>
    /// Список бронирований зала за указанный месяц
    /// </returns>
    /// <exception cref="NotFoundException"
    /// >Зал с указанным идентификатором не найден
    /// </exception>
    public async Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(
        int hallId,
        DateOnly dateInMonth)
    {
        logger.LogDebug("Поиск бронирований зала {HallId} за месяц {DateInMonth}", hallId, dateInMonth);
        await EnsureHallExistsAsync(hallId);

        var bookings = await bookingRepository.GetAllAsync();
        return [.. bookings
            .Where(booking =>
                booking.Hall.Id == hallId &&
                booking.DateTime.Year == dateInMonth.Year &&
                booking.DateTime.Month == dateInMonth.Month)
            .Select(booking => booking.ToDto())
            .OrderBy(booking => booking.DateTime)
            .ThenBy(booking => booking.Id)];
    }

    /// <summary>
    /// Проверяет, существует ли зал с указанным идентификатором
    /// </summary>
    /// <param name="hallId"
    /// >Идентификатор зала
    /// </param>
    /// <exception cref="NotFoundException"
    /// >Зал с указанным идентификатором не найден
    /// </exception>
    private async Task EnsureHallExistsAsync(int hallId)
    {
        if (await hallRepository.GetByIdAsync(hallId) is null)
        {
            logger.LogWarning("Зал {HallId} не найден", hallId);
            throw new NotFoundException($"Зал с ID {hallId} не найден.");
        }
    }
}
