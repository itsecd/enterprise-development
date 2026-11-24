using Hotel.Contracts.Dto;

namespace Hotel.Contracts.Interfaces;

/// <summary>
/// Описывает операции с гостиничными номерами
/// </summary>
public interface IRoomService
{
    public Task<IReadOnlyList<RoomDto>> GetAllAsync();
    public Task<RoomDto?> GetByIdAsync(int id);
    public Task<RoomDto> AddAsync(RoomEditDto room);
    public Task<bool> UpdateAsync(int id, RoomEditDto room);
    public Task<bool> DeleteAsync(int id);
    public Task<IReadOnlyList<RoomDto>> GetCurrentBookingsAsync(DateTime date);
    public Task<IReadOnlyList<RoomBookingCountDto>> GetMostBookedAsync();
    public Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountsAsync();
}
