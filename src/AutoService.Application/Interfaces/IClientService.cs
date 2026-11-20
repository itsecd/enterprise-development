using AutoService.Contracts.Dtos;

namespace AutoService.Application.Interfaces;

/// <summary>Операции с клиентами.</summary>
public interface IClientService : ICrudService<ClientRequest, ClientDto>
{
    /// <summary>Возвращает автомобили клиента.</summary>
    public Task<IReadOnlyList<CarDto>> GetCarsAsync(int id);
    /// <summary>Возвращает заказы клиента.</summary>
    public Task<IReadOnlyList<OrderDto>> GetOrdersAsync(int id);
}
