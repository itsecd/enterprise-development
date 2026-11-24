using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Application.Contracts.RoomTypes;

/// <summary>
/// Данные типа номера, возвращаемые через API
/// </summary>
public class RoomTypeResponse : IEntityResponse
{
    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Категория номера
    /// </summary>
    public RoomCategory Category { get; init; }

    /// <summary>
    /// Площадь номера, кв. м
    /// </summary>
    public double Area { get; init; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    public int BedCount { get; init; }

    /// <summary>
    /// Наличие ванны или душа
    /// </summary>
    public bool HasBathOrShower { get; init; }

    /// <summary>
    /// Стоимость проживания за сутки
    /// </summary>
    public decimal PricePerDay { get; init; }
}