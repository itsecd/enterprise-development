using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Orders;

/// <summary>
/// DTO для POST/PUT запросов к заказам
/// </summary>
public record OrderCreateUpdateDto(
    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор авто должен быть положительным")]
    int CarId,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор процедуры должен быть положительным")]
    int ProcedureId,

    DateTime StartTime,

    [property: Range(1, int.MaxValue, ErrorMessage = "Номер моечного бокса должен быть положительным")]
    int WashBoxNumber);