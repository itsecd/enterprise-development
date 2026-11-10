namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Клиент с повторными записями за период
/// </summary>
/// <param name="Client">Клиент</param>
/// <param name="AppointmentsCount">Общее количество записей клиента за период</param>
public record RepeatClientDto(ClientDto Client, int AppointmentsCount);