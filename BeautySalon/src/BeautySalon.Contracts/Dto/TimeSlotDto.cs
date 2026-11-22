namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Свободное окно в расписании мастера
/// </summary>
/// <param name="Start">Начало окна</param>
/// <param name="End">Конец окна</param>
public record TimeSlotDto(DateTime Start, DateTime End);