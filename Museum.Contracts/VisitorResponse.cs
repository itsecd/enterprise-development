namespace Museum.Contracts;

/// <summary>
/// Ответ с данными посетителя.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи.</param>
/// <param name="FullName">ФИО посетителя.</param>
/// <param name="Phone">Номер телефона.</param>
/// <param name="BirthDate">Дата рождения посетителя.</param>
public record VisitorResponse(
    int Id,
    string FullName,
    string Phone,
    DateOnly BirthDate
);
