using AutoServiceLab.Application.Contracts.DTOs.Mechanics;
using AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;

namespace AutoServiceLab.Application.Contracts.Interfaces;

public interface IMechanicService : IApplicationService<MechanicDto, MechanicCreateUpdateDto, int>
{
    /// <summary>
    /// Получить заказы, которые выполнял механик
    /// </summary>
    public IList<ServiceOrderDto> GetOrdersByMechanic(int mechanicId);
}
