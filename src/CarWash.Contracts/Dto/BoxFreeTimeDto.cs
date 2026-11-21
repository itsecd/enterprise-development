namespace CarWash.Contracts.Dto;

/// <summary>
/// Время освобождения бокса мойки
/// </summary>
public class BoxFreeTimeDto
{
    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int BoxNumber { get; set; }

    /// <summary>
    /// Время, начиная с которого бокс свободен
    /// </summary>
    public DateTime FreeTime { get; set; }
}
