using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий услуг автомойки
/// </summary>
public interface IWashServiceRepository : IRepository<WashService>
{
    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    Task<IReadOnlyList<(WashService WashService, int OrderCount)>> GetTop5PopularServicesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить общую выручку по каждой услуге
    /// </summary>
    Task<IReadOnlyList<(WashService WashService, decimal Revenue)>> GetRevenuePerServiceAsync(CancellationToken cancellationToken = default);
}
