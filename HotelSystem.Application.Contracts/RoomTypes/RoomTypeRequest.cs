using System.ComponentModel.DataAnnotations;
using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Application.Contracts.RoomTypes;

/// <summary>
/// Данные типа номера для создания и обновления
/// </summary>
public class RoomTypeRequest
{
    /// <summary>
    /// Категория номера
    /// </summary>
    [EnumDataType(typeof(RoomCategory))]
    public required RoomCategory Category { get; init; }

    /// <summary>
    /// Площадь номера, кв. м
    /// </summary>
    [Range(1, 1000)]
    public required double Area { get; init; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    [Range(1, 10)]
    public required int BedCount { get; init; }

    /// <summary>
    /// Наличие ванны или душа
    /// </summary>
    public bool HasBathOrShower { get; init; }

    /// <summary>
    /// Стоимость проживания за сутки
    /// </summary>
    [Range(0.01, 1_000_000)]
    public required decimal PricePerDay { get; init; }
}