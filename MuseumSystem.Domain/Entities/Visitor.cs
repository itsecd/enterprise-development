namespace MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

/// <summary>
/// Посетитель музея
/// </summary>
public class Visitor : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО посетителя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}