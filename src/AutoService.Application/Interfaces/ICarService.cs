using AutoService.Contracts.Dtos;

namespace AutoService.Application.Interfaces;

/// <summary>Операции с автомобилями.</summary>
public interface ICarService : ICrudService<CarRequest, CarDto>
{
    /// <summary>Возвращает заказы автомобиля.</summary>
    public Task<IReadOnlyList<OrderDto>> GetOrdersAsync(int id);
    /// <summary>Возвращает владельца автомобиля.</summary>
    public Task<ClientDto> GetOwnerAsync(int id);
}
