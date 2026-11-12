using System.ComponentModel.DataAnnotations;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Contracts.DTOs.Order;

/// <summary>
/// DTO для создания или обновления заказа
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="ItemId">Идентификатор изделия</param>
/// <param name="AcceptanceDate">Дата приема</param>
/// <param name="CompletionDays">Время выполнения</param>
/// <param name="Status">Статус заказа</param>
public record OrderCreateUpdateDto(
    int CustomerId,

    int ItemId,

    [Required(ErrorMessage = "Дата приема обязательна для заполнения!")]
    DateOnly AcceptanceDate,

    [Required(ErrorMessage = "Срок выполнения обязателен для заполнения!")]
    [Range(1, 365, ErrorMessage = "Некорректный срок выполнения!")]
    int CompletionDays,

    OrderStatus Status
);