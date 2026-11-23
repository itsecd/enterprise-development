using AutoServiceLab.Application.Contracts.Dtos.Cars;

namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Контракт для работы с автомобилями
/// </summary>
public interface ICarService : IApplicationService<CarDto, CarCreateUpdateDto, int>
{
    /// <summary>
    /// Получить автомобили конкретного клиента
    /// </summary>
    public Task<IList<CarDto>> GetCarsByClientId(int clientId);
}
