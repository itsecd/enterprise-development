using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;

namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления бронированиями
/// </summary>
public interface IBookingService : ICrudService<BookingRequest, BookingDto>
{
}