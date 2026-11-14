using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с услугами автомойки
/// </summary>
public class WashServiceService(IWashServiceRepository services, IOrderRepository orders) : IWashServiceService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<WashServiceResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await services.GetAllAsync(cancellationToken)).Select(service => service.ToResponse()).ToList();

    /// <inheritdoc />
    public async Task<WashServiceResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        (await services.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Услуга не найдена.")).ToResponse();

    /// <inheritdoc />
    public async Task<WashServiceResponse> CreateAsync(WashServiceRequest request, CancellationToken cancellationToken = default)
    {
        var service = request.ToDomain();
        await services.AddAsync(service, cancellationToken);
        return service.ToResponse();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, WashServiceRequest request, CancellationToken cancellationToken = default)
    {
        var service = await services.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Услуга не найдена.");
        var latestStart = DateTime.MaxValue.AddMinutes(-request.DurationMinutes);
        if (await orders.AnyAsync(order => order.WashServiceId == id && order.StartTime > latestStart,
                cancellationToken))
            throw new RequestValidationException(nameof(request.DurationMinutes),
                "Продолжительность услуги выводит время окончания существующего заказа за допустимые границы даты.");
        request.ApplyTo(service);
        await services.UpdateAsync(service, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        if (await orders.AnyAsync(order => order.WashServiceId == id, cancellationToken))
            throw new EntityConflictException("Услуга используется в заказах.");
        await services.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        return (await orders.GetByWashServiceIdAsync(id, cancellationToken)).Select(order => order.ToResponse()).ToList();
    }
}
