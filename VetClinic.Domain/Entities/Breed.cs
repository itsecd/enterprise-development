namespace VetClinic.Domain.Entities;

using VetClinic.Domain.Shared.Enums;
/// <summary>
/// Порода животного, зависит от вида
/// </summary>
public class Breed
{
    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название породы
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// Ссылка на соответствующий вид животного 
    /// </summary>
    public required Species Type { get; set; }
}
