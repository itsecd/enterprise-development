namespace BeautySalon.Application.Contracts.Analytics;

/// <summary>
/// Dto для свободного окошка у мастера
/// </summary>
/// <param name="Start">Начало окошка</param>
/// <param name="End">Конец окошка</param>
public record FreeSlotDto(DateTime Start, DateTime End);
