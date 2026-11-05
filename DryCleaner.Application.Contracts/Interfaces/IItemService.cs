using DryCleaner.Application.Contracts.DTOs.Item;

namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для работы с изделиями химчистки
/// </summary>
public interface IItemService
    : IApplicationService<ItemDto, ItemCreateUpdateDto, int>
{
    IList<ItemDto> GetByCategoryId(int categoryId);
}