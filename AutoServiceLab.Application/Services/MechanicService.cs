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
    public async Task<IList<MechanicDto>> GetAllAsync()
    {
        logger.LogInformation("Получение списка всех механиков");
        return (await repository.GetAllAsync())
            .Select(m => new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<MechanicDto?> GetAsync(int id)
    {
        logger.LogInformation("Получение механика с ID: {Id}", id);
        var m = await repository.GetAsync(id);
        return m == null ? null : new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears);
    }

    /// <inheritdoc />
    public async Task<MechanicDto> CreateAsync(MechanicCreateUpdateDto dto)
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
        await repository.AddAsync(mechanic);
        return new MechanicDto(mechanic.Id, mechanic.FullName, mechanic.Specialization, mechanic.ExperienceYears);
    }

    /// <inheritdoc />
    public async Task<MechanicDto> UpdateAsync(MechanicCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление механика с ID: {Id}", id);
        var m = await repository.GetAsync(id) ?? throw new KeyNotFoundException();
        m.FullName = dto.FullName;
        m.PassportNumber = dto.PassportNumber;
        m.Phone = dto.Phone;
        m.Specialization = dto.Specialization;
        m.ExperienceYears = dto.ExperienceYears;
        await repository.UpdateAsync(m);
        return new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Удаление механика с ID: {Id}", id);
        return await repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IList<ServiceOrderDto>> GetOrdersByMechanic(int mechanicId)
    {
        logger.LogInformation("Получение заказов механика с ID: {MechanicId}", mechanicId);

        return (await orderMechanicRepository.GetAllAsync())
            .Where(om => om.MechanicId == mechanicId && om.ServiceOrder != null)
            .Select(om => om.ServiceOrder!)
            .Distinct()
            .Select(o => new ServiceOrderDto(
                o.Id,
                o.ReceptionDate,
                o.IssueDate,
                new ClientDto(o.Client.Id, o.Client.FullName, o.Client.Phone),
                new CarDto(o.Car.Id, o.Car.LicensePlate, o.Car.Brand, o.Car.Model, o.Car.Year, o.Car.ClientId),
                o.OrderWorks.Select(ow => new WorkTypeDto(ow.WorkType.Id, ow.WorkType.Name, ow.WorkType.Category, ow.WorkType.Duration, ow.WorkType.LaborCost)).ToList(),
                o.OrderMechanics.Select(om => new MechanicDto(om.Mechanic.Id, om.Mechanic.FullName, om.Mechanic.Specialization, om.Mechanic.ExperienceYears)).ToList()
            ))
            .ToList();
    }
}
