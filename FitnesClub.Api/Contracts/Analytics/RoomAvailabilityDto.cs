namespace FitnesClub.Api.Contracts.Analytics;

/// <summary>
/// Результат проверки доступности зала на указанное время.
/// </summary>
/// <param name="RoomName">Название проверяемого зала.</param>
/// <param name="CheckDateTime">Дата и время, на которые выполнялась проверка.</param>
/// <param name="IsAvailable">Флаг доступности зала (true — свободен, false — занят).</param>
public record RoomAvailabilityDto(
    string RoomName,
    DateTime CheckDateTime,
    bool IsAvailable
);