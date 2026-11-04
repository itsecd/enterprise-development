using DryCleaner.Application.Contracts.DTOs.Item;

namespace DryCleaner.Application.Contracts.Interfaces;

public interface IItemService
    : IApplicationService<ItemDto, ItemCreateUpdateDto, int>
{
    IList<ItemDto> GetByCategoryId(int categoryId);
}