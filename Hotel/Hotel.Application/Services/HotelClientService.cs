using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.HotelClients;
using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с клиентами гостиницы
/// </summary>
public class HotelClientService(IRepository<HotelClient, int> clientRepository, IBookingRepository bookingRepository) : IHotelClientService
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<HotelClientDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var clients = await clientRepository.GetAll(cancellationToken);

        return clients
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<HotelClientDto?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetById(id, cancellationToken);

        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    /// <param name="dto">Данные для создания клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<HotelClientDto> Create(CreateHotelClientDto dto, CancellationToken cancellationToken = default)
    {
        var client = new HotelClient
        {
            Id = 0,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            BirthDate = dto.BirthDate,
            Citizenship = dto.Citizenship
        };

        var createdClient = await clientRepository.Add(client, cancellationToken);

        return ToDto(createdClient);
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="dto">Данные для обновления клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<HotelClientDto?> Update(int id, CreateHotelClientDto dto, CancellationToken cancellationToken = default)
    {
        var client = new HotelClient
        {
            Id = id,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            BirthDate = dto.BirthDate,
            Citizenship = dto.Citizenship
        };

        var updatedClient = await clientRepository.Update(client, cancellationToken);

        return updatedClient is null ? null : ToDto(updatedClient);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        return clientRepository.Delete(id, cancellationToken);
    }

    /// <summary>
    /// Получить бронирования указанного клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<BookingDto>?> GetBookingsByClientId(int clientId, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetById(clientId, cancellationToken);

        if (client is null) return null;

        var bookings = await bookingRepository.GetAll(cancellationToken);

        return bookings
            .Where(booking => booking.Client.Id == clientId)
            .Select(booking => new BookingDto(
                booking.Id,
                booking.Client.Id,
                booking.Room.Id,
                booking.CheckInDate,
                booking.DaysCount))
            .ToList();
    }

    /// <summary>
    /// Получить пять клиентов с наибольшей общей стоимостью проживания
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<HotelClientStayCostDto>> GetTopFiveClientsByTotalStayCost(CancellationToken cancellationToken = default)
    {
        var topClients = await bookingRepository.QueryAsync(bookings => bookings
            .GroupBy(booking => booking.Client.Id)
            .Select(group => new
            {
                ClientId = group.Key,
                group.First().Client,
                TotalStayCost = group.Sum(booking =>
                    booking.Room.RoomType.PricePerDay * booking.DaysCount)
            })
            .OrderByDescending(item => item.TotalStayCost)
            .ThenBy(item => item.ClientId)
            .Take(5), cancellationToken);

        return topClients
            .Select(item => new HotelClientStayCostDto(ToDto(item.Client), item.TotalStayCost))
            .ToList();
    }

    /// <summary>
    /// Получить клиентов, бронировавших номер указанной категории
    /// </summary>
    /// <param name="category">Категория номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<HotelClientDto>> GetClientsByRoomCategory(RoomCategory category, CancellationToken cancellationToken = default)
    {
        var clients = await bookingRepository.QueryAsync(bookings => bookings
            .Where(booking => booking.Room.RoomType.Category == category)
            .Select(booking => booking.Client)
            .Distinct()
            .OrderBy(client => client.Id), cancellationToken);

        return clients.Select(ToDto).ToList();
    }

    /// <summary>
    /// Преобразовать доменную сущность в DTO
    /// </summary>
    private static HotelClientDto ToDto(HotelClient client)
    {
        return new HotelClientDto(
            client.Id,
            client.PassportNumber,
            client.FirstName,
            client.LastName,
            client.MiddleName,
            client.BirthDate,
            client.Citizenship
        );
    }
}