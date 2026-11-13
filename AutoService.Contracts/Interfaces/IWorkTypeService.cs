using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с видами работ.
/// </summary>
public interface IWorkTypeService
{
    public Task<IEnumerable<WorkTypeDto>> GetAllAsync();
    public Task<WorkTypeDto?> GetByIdAsync(Guid id);
    public Task<WorkTypeDto> CreateAsync(WorkTypeDto workDto);
    public Task UpdateAsync(Guid id, WorkTypeUpdateDto workDto);
    public Task DeleteAsync(Guid id);
}