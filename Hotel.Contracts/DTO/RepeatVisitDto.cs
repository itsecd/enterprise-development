namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о повторных визитах клиента
/// </summary>
/// <param name="ClientId">Id клиента</param>
/// <param name="Count">Количество повторных визитов</param>
public sealed record RepeatVisitDto(int ClientId, decimal Count);