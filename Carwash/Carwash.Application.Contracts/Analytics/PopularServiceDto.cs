using Carwash.Application.Contracts.Services;

namespace Carwash.Application.Contracts.Analytics;

/// <summary>
/// Dto для запроса вывода услуги с количеством заказов с этой услугой
/// </summary>
/// <param name="Service">Dto услуги</param>
/// <param name="OrderCount">Количество заказов</param>
public record PopularServiceDto(
    ServiceDto Service,
    int OrderCount
);
