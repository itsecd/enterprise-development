using System.ComponentModel.DataAnnotations;
using CarWash.Shared.Enums;

namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные для создания и изменения услуги
/// </summary>
public class ServiceEditDto
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required(ErrorMessage = "Название услуги обязательно")]
    public required string Name { get; set; }

    /// <summary>
    /// Категория автомобиля, для которого предназначена услуга
    /// </summary>
    [EnumDataType(typeof(CarCategory), ErrorMessage = "Недопустимая категория автомобиля")]
    public CarCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги в рублях
    /// </summary>
    [Range(0.01, 100000, ErrorMessage = "Цена должна быть больше нуля")]
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    [Range(1, 1440, ErrorMessage = "Длительность должна быть от 1 до 1440 минут")]
    public int DurationMinutes { get; set; }
}
