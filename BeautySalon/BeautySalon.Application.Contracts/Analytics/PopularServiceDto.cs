using BeautySalon.Application.Contracts.Services;

namespace BeautySalon.Application.Contracts.Analytics;

/// <summary>
/// Dto для получения популярных услуг с количеством записей с этой услугой
/// </summary>
/// <param name="Service">Dto услуги</param>
/// <param name="AppointmentsCount">Количество записей</param>
public record PopularServiceDto(ServiceDto Service, int AppointmentsCount);
