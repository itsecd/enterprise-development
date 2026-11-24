namespace HotelSystem.Application.Contracts.Analytics;

/// <summary>
/// Информация о клиенте и суммарной стоимости его проживания
/// </summary>
public class TopClientByCostResponse
{
    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя клиента
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Суммарная стоимость проживания
    /// </summary>
    public decimal TotalCost { get; set; }
}