using Hotel.Contracts.Dto;

namespace Hotel.Contracts.Interfaces;

/// <summary>
/// Описывает операции с бронированиями
/// </summary>
public interface IBookingService
{
    public Task<IReadOnlyList<BookingDto>> GetAllAsync();
    public Task<BookingDto?> GetByIdAsync(int id);
    public Task<BookingDto> AddAsync(BookingEditDto booking);
    public Task<bool> UpdateAsync(int id, BookingEditDto booking);
    public Task<bool> DeleteAsync(int id);
}
