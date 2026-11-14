using CarWash.Application.Mapping;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Подготовка ответов аналитических запросов
/// </summary>
public class AnalyticsService(
    ICustomerRepository customers,
    IOrderRepository orders,
    IWashServiceRepository services) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<TopCustomerResponse>> GetTopCustomersAsync(CancellationToken cancellationToken = default) =>
        (await customers.GetTop5CustomersByVisitsAsync(cancellationToken)).Select(item => new TopCustomerResponse
        {
            CustomerId = item.Customer.Id,
            FullName = item.Customer.FullName,
            VisitCount = item.VisitCount
        }).ToList();

    /// <inheritdoc />
    public async Task<IReadOnlyList<CarResponse>> GetCurrentCarsAsync(CancellationToken cancellationToken = default) =>
        (await orders.GetCarsCurrentlyAtWashAsync(cancellationToken)).Select(car => car.ToResponse()).ToList();

    /// <inheritdoc />
    public async Task<IReadOnlyList<PopularServiceResponse>> GetTopServicesAsync(CancellationToken cancellationToken = default) =>
        (await services.GetTop5PopularServicesAsync(cancellationToken)).Select(item => new PopularServiceResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            OrderCount = item.OrderCount
        }).ToList();

    /// <inheritdoc />
    public async Task<BoxAvailabilityResponse> GetBoxAvailabilityAsync(int boxNumber, CancellationToken cancellationToken = default)
    {
        return new BoxAvailabilityResponse
        {
            BoxNumber = boxNumber,
            AvailableFrom = await orders.GetBoxAvailableFromAsync(boxNumber, cancellationToken)
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ServiceRevenueResponse>> GetRevenuePerServiceAsync(CancellationToken cancellationToken = default) =>
        (await services.GetRevenuePerServiceAsync(cancellationToken)).Select(item => new ServiceRevenueResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            Revenue = item.Revenue
        }).ToList();
}
