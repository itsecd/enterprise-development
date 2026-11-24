using Carwash.Application.Contracts.Services;

namespace Carwash.Application.Contracts.Analytics;

/// <summary>
/// Dto для вывода услуги и суммарной выручки по этой услуге
/// </summary>
/// <param name="Service">Dto услуги</param>
/// <param name="Revenue">Выручка</param>
public record ServiceRevenueDto(
    ServiceDto Service,
    decimal Revenue
);
