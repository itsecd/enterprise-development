namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Популярная услуга с количеством записей
/// </summary>
/// <param name="ServiceName">Название услуги</param>
/// <param name="AppointmentsCount">Количество записей на услугу</param>
public record PopularServiceDto(string ServiceName, int AppointmentsCount);