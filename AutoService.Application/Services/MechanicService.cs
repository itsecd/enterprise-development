using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для управления механиками.
/// </summary>
public class MechanicService(IMechanicRepository repository) : IMechanicService
{
    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    public async Task<IEnumerable<MechanicDto>> GetAllAsync()
    {
        var mechanics = await repository.GetAllAsync();
        return mechanics.Select(m => new MechanicDto
        {
            Id = m.Id, FullName = m.FullName, Passport = m.Passport,
            Specialization = m.Specialization.ToString(), Experience = m.Experience
        });
    }

    /// <summary>
    /// Получить механика по идентификатору.
    /// </summary>
    public async Task<MechanicDto?> GetByIdAsync(Guid id)
    {
        var m = await repository.GetByIdAsync(id);
        if (m == null) return null;
        return new MechanicDto
        {
            Id = m.Id, FullName = m.FullName, Passport = m.Passport,
            Specialization = m.Specialization.ToString(), Experience = m.Experience
        };
    }

    /// <summary>
    /// Добавить нового механика.
    /// </summary>
    public async Task<MechanicDto> CreateAsync(MechanicDto mechanicDto)
    {
        var mechanic = new Mechanic
        {
            Id = Guid.NewGuid(),
            FullName = mechanicDto.FullName,
            Passport = mechanicDto.Passport,
            Specialization = Enum.Parse<Specialization>(mechanicDto.Specialization),
            Experience = mechanicDto.Experience
        };
        await repository.AddAsync(mechanic);
        mechanicDto.Id = mechanic.Id;
        return mechanicDto;
    }

    /// <summary>
    /// Обновить данные механика.
    /// </summary>
    public async Task UpdateAsync(Guid id, MechanicUpdateDto mechanicDto)
    {
        var mechanic = await repository.GetByIdAsync(id);
        if (mechanic == null) return;

        mechanic.FullName = mechanicDto.FullName;
        mechanic.Passport = mechanicDto.Passport;
        mechanic.Specialization = Enum.Parse<Specialization>(mechanicDto.Specialization);
        mechanic.Experience = mechanicDto.Experience;
        await repository.UpdateAsync(mechanic);
    }

    /// <summary>
    /// Удалить механика.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var mechanic = await repository.GetByIdAsync(id);
        if (mechanic == null) return;
        await repository.DeleteAsync(mechanic);
    }
}