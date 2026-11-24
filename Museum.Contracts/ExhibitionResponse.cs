using Museum.Domain.Enums;

namespace Museum.Contracts;

/// <summary>
/// Ответ с данными выставки.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи.</param>
/// <param name="Name">Название выставки.</param>
/// <param name="Theme">Тематика выставки.</param>
/// <param name="HallNumber">Номер зала.</param>
/// <param name="StartDate">Дата начала выставки.</param>
/// <param name="EndDate">Дата окончания выставки.</param>
public record ExhibitionResponse(
    int Id,
    string Name,
    ExhibitionTheme Theme,
    int HallNumber,
    DateOnly StartDate,
    DateOnly EndDate
);
