namespace CarWash.Contracts.Dto;

/// <summary>
/// Клиент и количество его посещений
/// </summary>
public class TopClientDto
{
    /// <summary>
    /// Данные клиента
    /// </summary>
    public required ClientDto Client { get; set; }

    /// <summary>
    /// Количество посещений
    /// </summary>
    public int VisitCount { get; set; }
}
