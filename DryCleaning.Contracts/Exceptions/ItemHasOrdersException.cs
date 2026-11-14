namespace DryCleaning.Contracts.Exceptions;

public class ItemHasOrdersException(int itemId)
    : InvalidOperationException("Нельзя удалить изделие, с которым связаны заказы")
{
    public int ItemId { get; } = itemId;
}
