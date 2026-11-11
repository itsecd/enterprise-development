using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует операции для работы с залами фитнес-клуба.
/// </summary>
/// <param name="repository">Репозиторий залов.</param>
public class HallService(IHallRepository repository) : IHallService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<HallDto>> GetAllAsync()
    {
        var halls = await repository.GetAllAsync();

        return [.. halls.Select(hall => hall.ToDto())];
    }

    /// <inheritdoc />
    public async Task<HallDto?> GetByIdAsync(int id)
    {
        var hall = await repository.GetByIdAsync(id);

        return hall?.ToDto();
    }

    /// <inheritdoc />
    public async Task<HallDto> CreateAsync(CreateHallDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var hall = new Hall
        {
            Id = 0,
            Name = dto.Name,
            Capacity = dto.Capacity
        };

        var created = await repository.AddAsync(hall);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, UpdateHallDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var hall = await repository.GetByIdAsync(id);

        if (hall is null)
        {
            return false;
        }

        hall.Name = dto.Name;
        hall.Capacity = dto.Capacity;

        return await repository.UpdateAsync(id, hall);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        return repository.DeleteAsync(id);
    }
}