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
    public List<BookingResponse> GetAll() =>
        bookingRepository.GetAll().Select(booking => booking.ToResponse()).ToList();

    /// <inheritdoc />
    public BookingResponse? GetById(int id) =>
        bookingRepository.GetById(id)?.ToResponse();

    /// <inheritdoc />
    public BookingResponse Add(BookingRequest request) =>
        bookingRepository.Add(CreateEntity(request)).ToResponse();

    /// <inheritdoc />
    public bool Update(int id, BookingRequest request)
    {
        if (bookingRepository.GetById(id) is null)
        {
            return false;
        }

        return bookingRepository.Update(id, CreateEntity(request));
    }

    /// <inheritdoc />
    public bool Delete(int id) =>
        bookingRepository.Delete(id);

    /// <summary>
    /// Возвращает бронирования клиента
    /// </summary>
    public List<BookingResponse>? GetByClientId(int clientId)
    {
        if (clientRepository.GetById(clientId) is null)
        {
            return null;
        }

        return bookingRepository.GetAll()
            .Where(booking => booking.Client.Id == clientId)
            .Select(booking => booking.ToResponse())
            .ToList();
    }

    /// <summary>
    /// Возвращает бронирования номера
    /// </summary>
    public List<BookingResponse>? GetByRoomId(int roomId)
    {
        if (roomRepository.GetById(roomId) is null)
        {
            return null;
        }

        return bookingRepository.GetAll()
            .Where(booking => booking.Room.Id == roomId)
            .Select(booking => booking.ToResponse())
            .ToList();
    }

    private Booking CreateEntity(BookingRequest request)
    {
        var client = clientRepository.GetById(request.ClientId)
            ?? throw new ReferenceNotFoundException("Клиент", request.ClientId);

        var room = roomRepository.GetById(request.RoomId)
            ?? throw new ReferenceNotFoundException("Номер", request.RoomId);

        return request.ToEntity(client, room);
    }
}