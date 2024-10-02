using DispatchTransportControl.Api.DTO;

namespace DispatchTransportControl.Api.Service;

/// <summary>
///     Интерфейс сервиса для работы с назначениями водителей и транспортных средств на маршруты
/// </summary>
public interface IRouteAssignmentService
{
    /// <summary>
    ///     Получение всех назначений водителей и транспортных средств на маршруты
    /// </summary>
    public IEnumerable<RouteAssignmentDto> GetAll();

    /// <summary>
    ///     Получение назначения водителя и транспортного средства на маршрут по id
    /// </summary>
    public RouteAssignmentDto? GetById(int id);

    /// <summary>
    ///     Создание назначения водителя и транспортного средства на маршрут
    /// </summary>
    public RouteAssignmentDto Create(RouteAssignmentCreateDto dto);

    /// <summary>
    ///     Изменение существующего назначения водителя и транспортного средства на маршрут
    /// </summary>
    public RouteAssignmentDto Update(RouteAssignmentUpdateDto dto);

    /// <summary>
    ///     Удаление назначения водителя и транспортного средства на маршрут
    /// </summary>
    public void Delete(int id);
}