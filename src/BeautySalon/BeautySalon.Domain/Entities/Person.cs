using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Базовый абстрактный класс, 
/// представляющий человека (общие свойства для клиента и мастера).
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор человека.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество. Может отсутствовать.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Полное ФИО (собирается из Фамилии, Имени и Отчества).
    /// </summary>
    public string FullName => string.Join(" ",
        new[] { LastName, FirstName, Patronymic }
        .Where(s => !string.IsNullOrWhiteSpace(s)));
}
