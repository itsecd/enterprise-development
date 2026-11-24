namespace Hotel.Contracts.Dto;

/// <summary>
/// Клиент и суммарная стоимость его проживаний
/// </summary>
public class TopClientSpendingDto
{
    /// <summary>
    /// Клиент
    /// </summary>
    public required ClientDto Client { get; set; }

    /// <summary>
    /// Суммарная стоимость проживания
    /// </summary>
    public decimal TotalSpent { get; set; }
}
