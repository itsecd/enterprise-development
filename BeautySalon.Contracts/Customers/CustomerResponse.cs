using BeautySalon.Contracts.Interfaces;

using BeautySalon.Domain.Shared.Enums;

namespace BeautySalon.Contracts.Customers;

/// <summary>
/// Сведения о посетителе
/// </summary>
public sealed class CustomerResponse : IResourceResponse
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество при наличии
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Пол посетителя
    /// </summary>
    public required Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly DateOfBirth { get; init; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public string? PhoneNumber { get; init; }
}
