namespace DryCleaning.Contracts.Exceptions;

public class CategoryHasItemsException(int categoryId)
    : InvalidOperationException("Нельзя удалить категорию, которая используется изделиями")
{
    public int CategoryId { get; } = categoryId;
}
