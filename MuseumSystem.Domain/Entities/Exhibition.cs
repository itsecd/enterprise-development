using MuseumSystem.Shared.Enums;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Domain.Entities;

/// <summary>
/// Выставка музея
/// </summary>
public class Exhibition : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тематика выставки
    /// </summary>
    public required ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Номер зала, в котором проходит выставка
    /// </summary>
    public required int HallNumber { get; set; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public required DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public required DateOnly EndDate { get; set; }
}