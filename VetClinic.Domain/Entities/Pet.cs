namespace VetClinic.Domain.Entities;

using VetClinic.Domain.Shared.Enums;
/// <summary>
/// Питомец
/// </summary>
public class Pet
{
    /// <summary>
    /// Идентификатор питомца 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Кличка животного 
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Вид животного
    /// </summary>
    public required Species Type { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int BreedId { get; set; }
    /// <summary>
    /// Ссылка на породу животного 
    /// </summary>
    public required Breed Breed { get; set; }
    /// <summary>
    /// Вес животного 
    /// </summary>
    public required decimal Weight { get; set; }
    /// <summary>
    /// Идентификатор владельца 
    /// </summary>
    public int OwnerId { get; set; }
    /// <summary>
    /// Ссылка на владельца питомца 
    /// </summary>
    public required Owner Owner { get; set; }
}