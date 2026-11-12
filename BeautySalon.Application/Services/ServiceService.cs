using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Services;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Application.Services;

/// <summary>
/// Операции с сущностями Service
/// </summary>
/// <param name="repository">Репозиторий сущностей</param>
public sealed class ServiceService(IServiceRepository repository) : IServiceService
{
    /// <summary>
    /// Получить список объектов
    /// </summary>
    public IReadOnlyList<ServiceResponse> GetAll()
    {
        return repository.GetAll().Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public ServiceResponse GetById(int id)
    {
        var entity = repository.GetById(id)
            ?? throw new KeyNotFoundException("Объект не найден");
        return ToResponse(entity);
    }

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    public ServiceResponse Create(SaveServiceRequest request)
    {
        var entity = ToEntity(0, request);
        return ToResponse(repository.Add(entity));
    }

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    public void Update(int id, SaveServiceRequest request)
    {
        var entity = ToEntity(id, request);
        if (!repository.Update(entity))
        {
            throw new KeyNotFoundException("Объект не найден");
        }
    }

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public void Delete(int id)
    {
        if (!repository.Delete(id))
        {
            throw new KeyNotFoundException("Объект не найден");
        }
    }

    private static Service ToEntity(int id, SaveServiceRequest request)
    {

        return new Service
        {
            Id = id,
            Name = request.Name.Trim(),
            Category = request.Category?.Trim(),
            Price = request.Price!.Value,
            DurationMinutes = request.DurationMinutes!.Value,
        };
    }

    private static ServiceResponse ToResponse(Service entity)
    {
        return new ServiceResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            Price = entity.Price,
            DurationMinutes = entity.DurationMinutes,
        };
    }
}