namespace FitnessClub.Domain.Entities;
/// <summary>
/// Базовый класс для всех сущностей
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
}