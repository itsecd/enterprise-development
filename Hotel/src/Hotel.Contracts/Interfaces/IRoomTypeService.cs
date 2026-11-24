using Hotel.Contracts.Dto;

namespace Hotel.Contracts.Interfaces;

/// <summary>
/// Описывает операции со справочником типов номеров
/// </summary>
public interface IRoomTypeService
{
    public Task<IReadOnlyList<RoomTypeDto>> GetAllAsync();
    public Task<RoomTypeDto?> GetByIdAsync(int id);
    public Task<RoomTypeDto> AddAsync(RoomTypeEditDto roomType);
    public Task<bool> UpdateAsync(int id, RoomTypeEditDto roomType);
    public Task<bool> DeleteAsync(int id);
}
