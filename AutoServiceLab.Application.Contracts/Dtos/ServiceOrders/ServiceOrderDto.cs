using AutoServiceLab.Application.Contracts.Dtos.Cars;
using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;

namespace AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;

/// <summary>
/// DTO для заказа на обслуживание
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="ReceptionDate">Дата приема автомобиля</param>
/// <param name="IssueDate">Дата выдачи автомобиля</param>
/// <param name="Client">Клиент, оформивший заказ</param>
/// <param name="Car">Автомобиль, принятый в обслуживание</param>
/// <param name="Works">Список работ в заказе</param>
/// <param name="Mechanics">Список механиков, выполнявших заказ</param>
public record ServiceOrderDto(
    int Id,
    DateTime ReceptionDate,
    DateTime? IssueDate,
    ClientDto Client,
    CarDto Car,
    IList<WorkTypeDto> Works,
    IList<MechanicDto> Mechanics
);
