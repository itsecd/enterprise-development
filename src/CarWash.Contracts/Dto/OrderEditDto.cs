using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные для создания и изменения заказа
/// </summary>
public class OrderEditDto
{
    /// <summary>
    /// ID клиента
    /// </summary>
    [Required(ErrorMessage = "ID клиента обязателен")]
    public int ClientId { get; set; }

    /// <summary>
    /// ID автомобиля
    /// </summary>
    [Required(ErrorMessage = "ID автомобиля обязателен")]
    public int CarId { get; set; }

    /// <summary>
    /// ID услуги
    /// </summary>
    [Required(ErrorMessage = "ID услуги обязателен")]
    public int ServiceId { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    [Required(ErrorMessage = "Время начала обязательно")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    [Range(1, 100, ErrorMessage = "Номер бокса должен быть от 1 до 100")]
    public int BoxNumber { get; set; }
}
