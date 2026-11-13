namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о клиенте и его общей стоимости проживания
/// </summary>
/// <param name="ClientId">Id клиента</param>
/// <param name="FullName">ФИО клиента</param>
/// <param name="TotalCost">Суммарная стоимость проживания</param>
public sealed record TopClientDto(int ClientId, string FullName, decimal TotalCost);