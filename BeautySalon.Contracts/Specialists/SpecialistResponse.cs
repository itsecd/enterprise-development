using BeautySalon.Contracts.Interfaces;

using BeautySalon.Domain.Shared.Enums;

namespace BeautySalon.Contracts.Specialists;

/// <summary>
/// Сведения о специалисте
/// </summary>
public sealed class SpecialistResponse : IResourceResponse
{
    /// <summary>
    /// Идентификатор специалиста
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
    /// Пол специалиста
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

    /// <summary>
    /// Паспортные данные
    /// </summary>
    public string? PassportNumber { get; init; }

    /// <summary>
    /// Специализация
    /// </summary>
    public string? Specialization { get; init; }

    /// <summary>
    /// Стаж работы в полных годах
    /// </summary>
    public required int WorkExperience { get; init; }
}
