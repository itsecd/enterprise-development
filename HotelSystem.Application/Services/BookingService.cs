using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Exceptions;
using HotelSystem.Application.Mapping;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с бронированиями
/// </summary>
public class BookingService(
    IRepository<Booking> bookingRepository,
    IRepository<Client> clientRepository,
    IRepository<Room> roomRepository)
    : ICrudService<BookingRequest, BookingResponse>
{
    /// <inheritdoc />
    public async Task<List<BookingResponse>> GetAllAsync() =>
        (await bookingRepository.GetAllAsync())
            .Select(booking => booking.ToResponse())
            .ToList();

    /// <inheritdoc />
    public async Task<BookingResponse?> GetByIdAsync(int id) =>
        (await bookingRepository.GetByIdAsync(id))?.ToResponse();

    /// <inheritdoc />
    public async Task<BookingResponse> AddAsync(BookingRequest request) =>
        (await bookingRepository.AddAsync(await CreateEntityAsync(request))).ToResponse();

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, BookingRequest request)
    {
        if (await bookingRepository.GetByIdAsync(id) is null)
        {
            return false;
        }

        return await bookingRepository.UpdateAsync(id, await CreateEntityAsync(request));
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id) =>
        await bookingRepository.DeleteAsync(id);

    /// <summary>
    /// Возвращает бронирования клиента
    /// </summary>
    public async Task<List<BookingResponse>?> GetByClientIdAsync(int clientId)
    {
        if (await clientRepository.GetByIdAsync(clientId) is null)
        {
            return null;
        }

        return (await bookingRepository.GetAllAsync())
            .Where(booking => booking.Client.Id == clientId)
            .Select(booking => booking.ToResponse())
            .ToList();
    }

    /// <summary>
    /// Возвращает бронирования номера
    /// </summary>
    public async Task<List<BookingResponse>?> GetByRoomIdAsync(int roomId)
    {
        if (await roomRepository.GetByIdAsync(roomId) is null)
        {
            return null;
        }

        return (await bookingRepository.GetAllAsync())
            .Where(booking => booking.Room.Id == roomId)
            .Select(booking => booking.ToResponse())
            .ToList();
    }

    private async Task<Booking> CreateEntityAsync(BookingRequest request)
    {
        var client = await clientRepository.GetByIdAsync(request.ClientId)
            ?? throw new ReferenceNotFoundException("Клиент", request.ClientId);

        var room = await roomRepository.GetByIdAsync(request.RoomId)
            ?? throw new ReferenceNotFoundException("Номер", request.RoomId);

        return request.ToEntity(client, room);
    }
}