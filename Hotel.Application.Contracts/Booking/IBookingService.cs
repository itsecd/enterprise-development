namespace Hotel.Application.Contracts.Booking;
/// <summary>
/// Наследний интерфейса служды для CRUD операций над бронями
/// </summary>
public interface IBookingService :
    IService<BookingResponseDto, BookingCreateUpdateDto>
{ }