using AutoServiceLab.Application.Contracts.Dtos.Cars;
using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для работы с механиками
/// </summary>
public class MechanicService(
    IMechanicRepository repository,
    IOrderMechanicRepository orderMechanicRepository,
    ILogger<MechanicService> logger) : IMechanicService
{
    /// <inheritdoc />
    public IList<MechanicDto> GetAll()
    {
        logger.LogInformation("Получение списка всех механиков");
        return repository.GetAll()
            .Select(m => new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears))
            .ToList();
    }

    /// <inheritdoc />
    public MechanicDto? Get(int id)
    {
        logger.LogInformation("Получение механика с ID: {Id}", id);
        var m = repository.Get(id);
        return m == null ? null : new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears);
    }

    /// <inheritdoc />
    public MechanicDto Create(MechanicCreateUpdateDto dto)
    {
        logger.LogInformation("Создание механика: {FullName}", dto.FullName);
        var mechanic = new Mechanic
        {
            FullName = dto.FullName,
            PassportNumber = dto.PassportNumber,
            Phone = dto.Phone,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears
        };
        repository.Add(mechanic);
        return new MechanicDto(mechanic.Id, mechanic.FullName, mechanic.Specialization, mechanic.ExperienceYears);
    }

    /// <inheritdoc />
    public MechanicDto Update(MechanicCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление механика с ID: {Id}", id);
        var m = repository.Get(id) ?? throw new KeyNotFoundException();
        m.FullName = dto.FullName;
        m.PassportNumber = dto.PassportNumber;
        m.Phone = dto.Phone;
        m.Specialization = dto.Specialization;
        m.ExperienceYears = dto.ExperienceYears;
        repository.Update(m);
        return new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление механика с ID: {Id}", id);
        return repository.Delete(id);
    }

    /// <inheritdoc />
    public IList<ServiceOrderDto> GetOrdersByMechanic(int mechanicId)
    {
        logger.LogInformation("Получение заказов механика с ID: {MechanicId}", mechanicId);
        return orderMechanicRepository.GetAll()
            .Where(om => om.MechanicId == mechanicId)
            .Select(om => om.ServiceOrder)
            .Distinct()
            .Select(o => new ServiceOrderDto(
                o.Id,
                o.ReceptionDate,
                o.IssueDate,
                new ClientDto(o.Client.Id, o.Client.FullName, o.Client.Phone),
                new CarDto(o.Car.Id, o.Car.LicensePlate, o.Car.Brand, o.Car.Model, o.Car.Year, o.Car.ClientId),
                o.OrderWorks.Select(ow => new WorkTypeDto(ow.WorkType.Id, ow.WorkType.Name, ow.WorkType.Category, ow.WorkType.Duration, ow.WorkType.LaborCost)).ToList(),
                o.OrderMechanics.Select(m => new MechanicDto(m.Mechanic.Id, m.Mechanic.FullName, m.Mechanic.Specialization, m.Mechanic.ExperienceYears)).ToList()
            ))
            .ToList();
    }
}
