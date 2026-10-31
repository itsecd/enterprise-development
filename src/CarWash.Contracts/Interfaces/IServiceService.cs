using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

public interface IServiceService
{
    public Task<IEnumerable<ServiceDto>> GetAllAsync();
    public Task<ServiceDto?> GetByIdAsync(int id);
    public Task<ServiceDto> CreateAsync(ServiceEditDto editDto);
    public Task UpdateAsync(int id, ServiceEditDto editDto);
    public Task DeleteAsync(int id);
}