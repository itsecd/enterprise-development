namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO клиента из топа по стоимости перевозок
/// </summary>
public class TopClientDto
{
    /// <summary>
    /// ФИО клиента или наименование организации
    /// </summary>
    public required string ClientName { get; init; }

    /// <summary>
    /// Суммарная стоимость перевозок
    /// </summary>
    public required decimal TotalCost { get; init; }
}