using AutoService.Contracts.Dtos;

namespace AutoService.Application.Interfaces;

/// <summary>Операции с заказами.</summary>
public interface IOrderService : ICrudService<OrderRequest, OrderDto>
{
    /// <summary>Возвращает автомобиль заказа.</summary>
    public Task<CarDto> GetCarAsync(int id);
    /// <summary>Возвращает клиента заказа.</summary>
    public Task<ClientDto> GetClientAsync(int id);
    /// <summary>Возвращает механика заказа.</summary>
    public Task<MechanicDto> GetMechanicAsync(int id);
    /// <summary>Возвращает виды работ заказа.</summary>
    public Task<IReadOnlyList<WorkTypeDto>> GetWorkTypesAsync(int id);
    /// <summary>Рассчитывает суммарную стоимость заказа.</summary>
    public Task<OrderCostDto> GetTotalCostAsync(int id);
}
