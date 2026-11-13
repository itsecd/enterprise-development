namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Питомец клиники
/// </summary>
public class Pet
{
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Кличка питомца
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Порода питомца
    /// </summary>
    public required Breed Breed { get; set; }

    /// <summary>
    /// Дата рождения без времени
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Владелец питомца, если известен
    /// </summary>
    public Owner? Owner { get; set; }

    /// <summary>
    /// Меняет владельца и поддерживает согласованность обеих сторон связи
    /// </summary>
    /// <param name="owner">Новый владелец или null</param>
    public void ChangeOwner(Owner? owner)
    {
        if (ReferenceEquals(Owner, owner)) return;
        Owner?.Pets.Remove(this);
        Owner = owner;
        if (owner is not null && !owner.Pets.Contains(this)) owner.Pets.Add(this);
    }
}
