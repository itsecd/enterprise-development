using Carwash.Domain;
using Carwash.Domain.Entities;
using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Orders;

namespace Carwash.Application.Services;

/// <summary>
/// Служба для CRUD-операций над заказами
/// </summary>
/// <param name="orderRepository">Репозиторий заказов</param>
/// <param name="clientRepository">Репозиторий клиентов</param>
/// <param name="carRepository">Репозиторий автомобилей</param>
/// <param name="serviceRepository">Репозиторий услуг</param>
public class OrderService(IRepository<Order, int> orderRepository,
    IRepository<Client, int> clientRepository, IRepository<Car, int> carRepository,
    IRepository<Service, int> serviceRepository) :
    IApplicationService<OrderDto, OrderCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<OrderDto> Create(OrderCreateUpdateDto dto)
    {
        var client = await clientRepository.Read(dto.ClientId) ?? throw new KeyNotFoundException($"Клиент с Id={dto.ClientId} не существует");
        var car = await carRepository.Read(dto.CarId) ?? throw new KeyNotFoundException($"Автомобиль с Id={dto.CarId} не существует");
        var service = await serviceRepository.Read(dto.ServiceId) ?? throw new KeyNotFoundException($"Услуга с Id={dto.ServiceId} не существует");

        var order = Mapper.Map(dto, client, car, service);

        var result = await orderRepository.Create(order);

        return Mapper.Map(result);
    }
    
    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await orderRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<OrderDto?> Read(int id)
    {
        var order = await orderRepository.Read(id);

        if (order == null)
            return null;

        return Mapper.Map(order);
    }
    
    /// <inheritdoc/>
    public async Task<IList<OrderDto>> ReadAll() => Mapper.Map(await orderRepository.ReadAll());
    
    /// <inheritdoc/>
    public async Task<OrderDto> Update(OrderCreateUpdateDto dto, int id)
    {
        var client = await clientRepository.Read(dto.ClientId) ?? throw new KeyNotFoundException($"Клиент с Id={dto.ClientId} не существует");
        var car = await carRepository.Read(dto.CarId) ?? throw new KeyNotFoundException($"Автомобиль с Id={dto.CarId} не существует");
        var service = await serviceRepository.Read(dto.ServiceId) ?? throw new KeyNotFoundException($"Услуга с Id={dto.ServiceId} не существует");

        var order = Mapper.Map(dto, client, car, service);
        order.Id = id;

        var result = await orderRepository.Update(order);

        return Mapper.Map(result);
    }
}
