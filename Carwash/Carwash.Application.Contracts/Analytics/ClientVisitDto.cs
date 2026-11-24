using Carwash.Application.Contracts.Clients;

namespace Carwash.Application.Contracts.Analytics;

/// <summary>
/// Dto для запроса вывода клиентов с количеством посещений
/// </summary>
/// <param name="Client">Dto клиента</param>
/// <param name="VisitCount">Количество посещений</param>
public record ClientVisitDto(
    ClientDto Client,
    int VisitCount
);
