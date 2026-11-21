namespace CarWash.Contracts.Dto;

/// <summary>
/// Автомобиль, находящийся на мойке
/// </summary>
public class CurrentCarDto
{
    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Название оказываемой услуги
    /// </summary>
    public required string ServiceName { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int BoxNumber { get; set; }
}
