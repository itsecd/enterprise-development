namespace DryCleaner.Application.Contracts.Client;

/// <summary>
/// DTO для Post, Put запросов к клиентам
/// </summary>
/// <param name="Name">ФИО клиента</param>
/// <param name="Phone">Номер клиента</param>
public record ClientCreateUpdateDto(string Name, string? Phone);
