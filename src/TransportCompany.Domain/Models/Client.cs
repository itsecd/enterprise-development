using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Domain.Models;

/// <summary>
/// Клиент транспортной компании — ФИО или название организации
/// </summary>
public class Client
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО или название организации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Тип клиента
    /// </summary>
    public ClientType Type { get; set; }

    /// <summary>
    /// Форма Юр.лица
    /// </summary>
    public string? LegalForm { get; set; }

    /// <summary>
    /// Полное наименование для вывода
    /// </summary>
    public string DisplayName => Type switch
    {
        ClientType.LegalPerson => $"{LegalForm ?? "ООО"} «{Name}»",
        ClientType.IndividualEntrepreneur => $"ИП {Name}",
        _ => Name
    };
}