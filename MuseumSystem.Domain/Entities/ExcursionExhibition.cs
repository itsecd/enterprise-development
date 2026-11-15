namespace MuseumSystem.Domain.Entities;

/// <summary>
/// Промежуточная сущность для связи многие ко многим между экскурсией и выставкой.
/// </summary>
public class ExcursionExhibition
{
    public int ExcursionId { get; set; }
    public Excursion Excursion { get; set; } = null!;
    public int ExhibitionId { get; set; }
    public Exhibition Exhibition { get; set; } = null!;
}