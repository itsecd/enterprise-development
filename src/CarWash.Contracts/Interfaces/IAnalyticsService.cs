using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

public interface IAnalyticsService
{
    public Task<IEnumerable<TopClientDto>> GetTop5ClientsAsync();
    public Task<IEnumerable<CurrentCarDto>> GetCurrentCarsAsync(DateTime? time);
    public Task<IEnumerable<TopServiceDto>> GetTop5PopularServicesAsync();
    public Task<BoxFreeTimeDto> GetBoxFreeTimeAsync(int boxNumber, DateTime? time);
    public Task<Dictionary<string, decimal>> GetRevenueForEachServiceAsync();
}