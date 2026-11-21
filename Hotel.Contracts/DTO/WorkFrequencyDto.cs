namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о частоте бронирования комнаты
/// </summary>
/// <param name="RoomNumber">Номер комнаты</param>
/// <param name="Count">Количество бронирований</param>
public sealed record WorkFrequencyDto(int RoomNumber, int Count);