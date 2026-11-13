using Hotel.Domain.Interfaces;

namespace Hotel.Domain.Entities;

/// <summary>
/// Клиенты гостиницы
/// </summary>
public class Client : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    public required DateTime BirthDate { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string NumberPassport { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public required string Citizenship { get; set; }

}
