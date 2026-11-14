namespace DryCleaning.Contracts.Exceptions;

public class ClientHasOrdersException(int clientId)
    : InvalidOperationException("Нельзя удалить клиента, у которого есть заказы")
{
    public int ClientId { get; } = clientId;
}
