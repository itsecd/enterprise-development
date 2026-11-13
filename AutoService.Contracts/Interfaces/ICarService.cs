using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с автомобилями.
/// </summary>
public interface ICarService
{
    public Task<IEnumerable<CarDto>> GetAllAsync();
    public Task<CarDto?> GetByIdAsync(Guid id);
    public Task<CarDto> CreateAsync(CarDto carDto);
    public Task UpdateAsync(Guid id, CarUpdateDto carDto);
    public Task DeleteAsync(Guid id);
}