namespace Museum.Contracts;

/// <summary>Данные для создания или изменения экскурсии.</summary>
public class TourRequest
{
    /// <summary>Дата и время начала экскурсии.</summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>Продолжительность экскурсии.</summary>
    public TimeSpan Duration { get; set; }
}
