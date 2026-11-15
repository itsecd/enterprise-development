namespace VetClinic.Domain.Entities;

using VetClinic.Domain.Shared.Enums;
/// <summary>
/// Специализация врачей и оказываемых услуг
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// Вид животного
    /// </summary>
    public required Species TargetSpecies { get; set; }
}