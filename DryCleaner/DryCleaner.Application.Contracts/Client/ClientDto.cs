namespace DryCleaner.Application.Contracts.Client;

/// <summary>
/// DTO для Get запросов к клиентам
/// </summary>
/// <param name="Id">Идинтификатор</param>
/// <param name="Name">ФИО клиента</param>
/// <param name="Phone">Номер клиента</param>
public record ClientDto(int Id, string Name, string? Phone);
