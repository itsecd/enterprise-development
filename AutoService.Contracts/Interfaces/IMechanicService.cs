using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с механиками.
/// </summary>
public interface IMechanicService
{
    public Task<IEnumerable<MechanicDto>> GetAllAsync();
    public Task<MechanicDto?> GetByIdAsync(Guid id);
    public Task<MechanicDto> CreateAsync(MechanicDto mechanicDto);
    public Task UpdateAsync(Guid id, MechanicUpdateDto mechanicDto);
    public Task DeleteAsync(Guid id);
}