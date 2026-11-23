namespace Museum.Contracts;

/// <summary>
/// Данные о посетителе экскурсии.
/// </summary>
/// <param name="VisitorId">Идентификатор посетителя.</param>
/// <param name="FullName">ФИО посетителя.</param>
/// <param name="Phone">Номер телефона.</param>
/// <param name="BirthDate">Дата рождения посетителя.</param>
public record TourVisitorResponse(
    int VisitorId,
    string FullName,
    string Phone,
    DateOnly BirthDate
);
