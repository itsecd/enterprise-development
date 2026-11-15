using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;

namespace AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;

/// <summary>
/// DTO для заказа на обслуживание
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="ReceptionDate">Дата приема автомобиля</param>
/// <param name="IssueDate">Дата выдачи автомобиля</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientName">ФИО клиента</param>
/// <param name="CarId">Идентификатор автомобиля</param>
/// <param name="CarModel">Модель автомобиля</param>
/// <param name="Works">Список работ в заказе</param>
/// <param name="Mechanics">Список механиков, выполнявших заказ</param>
public record ServiceOrderDto(
    int Id,
    DateTime ReceptionDate,
    DateTime? IssueDate,
    int ClientId,
    string ClientName,
    int CarId,
    string CarModel,
    IList<WorkTypeDto> Works,
    IList<MechanicDto> Mechanics
);
