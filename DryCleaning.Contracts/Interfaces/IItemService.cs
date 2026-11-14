using DryCleaning.Contracts.Dto;

namespace DryCleaning.Contracts.Interfaces;

public interface IItemService
{
    Task<IReadOnlyList<ItemDto>> GetAllAsync();
    Task<IReadOnlyList<ItemDto>?> GetByCategoryIdAsync(int categoryId);
    Task<ItemDto?> GetByIdAsync(int id);
    Task<ItemDto> AddAsync(ItemEditDto item);
    Task<bool> UpdateAsync(int id, ItemEditDto item);
    Task<bool> DeleteAsync(int id);
}
