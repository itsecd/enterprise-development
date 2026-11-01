using AutoServiceLab.Application.Contracts.DTOs.Cars;

namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Контракт для работы с автомобилями
/// </summary>
public interface ICarService : IApplicationService<CarDto, CarCreateUpdateDto, int>
{
    /// <summary>
    /// Получить автомобили конкретного клиента
    /// </summary>
    public IList<CarDto> GetCarsByClientId(int clientId);
}
