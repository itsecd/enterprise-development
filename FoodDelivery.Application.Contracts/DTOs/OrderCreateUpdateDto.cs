using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления заказа
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="CreatedAt">Дата и время создания заказа</param>
/// <param name="DeliveredAt">Дата и время доставки заказа</param>
public record OrderCreateUpdateDto(
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор клиента должен быть положительным")]
    int CustomerId,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор ресторана должен быть положительным")]
    int RestaurantId,

    [Required(ErrorMessage = "Дата создания заказа обязательна")]
    [Range(typeof(DateTime), "01/01/2000", "12/31/9999", ParseLimitsInInvariantCulture = true, ErrorMessage = "Укажите корректную дату создания заказа")]
    DateTime CreatedAt,

    [Required(ErrorMessage = "Дата доставки обязательна")]
    [Range(typeof(DateTime), "01/01/2000", "12/31/9999", ParseLimitsInInvariantCulture = true, ErrorMessage = "Укажите корректную дату доставки")]
    DateTime DeliveredAt
);