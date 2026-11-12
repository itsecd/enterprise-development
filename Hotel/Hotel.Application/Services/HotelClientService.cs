using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.HotelClients;
using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с клиентами гостиницы
/// </summary>
public class HotelClientService(IRepository<HotelClient, int> clientRepository, IRepository<Booking, int> bookingRepository) : IHotelClientService
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    public async Task<IReadOnlyList<HotelClientDto>> GetAll()
    {
        var clients = await clientRepository.GetAll();

        return clients
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public async Task<HotelClientDto?> GetById(int id)
    {
        var client = await clientRepository.GetById(id);

        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    public async Task<HotelClientDto> Create(CreateHotelClientDto dto)
    {
        var clients = await clientRepository.GetAll();

        var newId = clients.Count == 0
            ? 1
            : clients.Max(client => client.Id) + 1;

        var client = new HotelClient
        {
            Id = newId,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            BirthDate = dto.BirthDate,
            Citizenship = dto.Citizenship
        };

        var createdClient = await clientRepository.Add(client);

        return ToDto(createdClient);
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    public async Task<HotelClientDto?> Update(int id, CreateHotelClientDto dto)
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

        var updatedClient = await clientRepository.Update(client);

        return updatedClient is null ? null : ToDto(updatedClient);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public Task<bool> Delete(int id)
    {
        return clientRepository.Delete(id);
    }

    /// <summary>
    /// Получить бронирования указанного клиента
    /// </summary>
    public async Task<IReadOnlyList<BookingDto>?> GetBookingsByClientId(int clientId)
    {
        var client = await clientRepository.GetById(clientId);

        if (client is null) return null;

        var bookings = await bookingRepository.GetAll();

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
    public async Task<IReadOnlyList<HotelClientStayCostDto>> GetTopFiveClientsByTotalStayCost()
    {
        var bookings = await bookingRepository.GetAll();

        return bookings
            .GroupBy(booking => booking.Client.Id)
            .Select(group => new HotelClientStayCostDto(
                ToDto(group.First().Client),
                group.Sum(booking =>
                    booking.Room.RoomType.PricePerDay * booking.DaysCount)))
            .OrderByDescending(item => item.TotalStayCost)
            .ThenBy(item => item.Client.Id)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Получить клиентов, бронировавших номер указанной категории
    /// </summary>
    public async Task<IReadOnlyList<HotelClientDto>> GetClientsByRoomCategory(RoomCategory category)
    {
        var bookings = await bookingRepository.GetAll();
        var clients = await clientRepository.GetAll();

        return clients
            .Where(client => bookings.Any(booking =>
                booking.Client.Id == client.Id &&
                booking.Room.RoomType.Category == category))
            .Select(ToDto)
            .ToList();
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