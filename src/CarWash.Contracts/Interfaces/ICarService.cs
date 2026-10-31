using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

public interface ICarService
{
    public Task<IEnumerable<CarDto>> GetAllAsync();
    public Task<CarDto?> GetByIdAsync(int id);
    public Task<CarDto> CreateAsync(CarEditDto editDto);
    public Task UpdateAsync(int id, CarEditDto editDto);
    public Task DeleteAsync(int id);
}