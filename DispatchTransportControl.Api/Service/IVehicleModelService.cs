using DispatchTransportControl.Api.DTO;

namespace DispatchTransportControl.Api.Service;

/// <summary>
///     Интерфейс сервиса для работы с моделями транспортных средств
/// </summary>
public interface IVehicleModelService
{
    /// <summary>
    ///     Получение всех моделей транспортных средств
    /// </summary>
    public IEnumerable<VehicleModelDto> GetAll();

    /// <summary>
    ///     Получение модели транспортного средства по id
    /// </summary>
    public VehicleModelDto? GetById(int id);

    /// <summary>
    ///     Создание модели транспортного средства
    /// </summary>
    public VehicleModelDto Create(VehicleModelCreateDto dto);

    /// <summary>
    ///     Изменение существующей модели транспортного средства
    /// </summary>
    public VehicleModelDto Update(VehicleModelDto dto);

    /// <summary>
    ///     Удаление модели транспортного средства
    /// </summary>
    public void Delete(int id);
}