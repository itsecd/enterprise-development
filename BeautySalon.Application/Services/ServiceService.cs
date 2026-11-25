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
    public async Task<IReadOnlyList<ServiceResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await repository.GetAllAsync(cancellationToken)).Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public async Task<ServiceResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Объект не найден");
        return ToResponse(entity);
    }

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    public async Task<ServiceResponse> CreateAsync(SaveServiceRequest request, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(0, request);
        return ToResponse(await repository.AddAsync(entity, cancellationToken));
    }

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    public async Task UpdateAsync(int id, SaveServiceRequest request, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(id, request);
        if (!await repository.UpdateAsync(entity, cancellationToken))
        {
            throw new KeyNotFoundException("Объект не найден");
        }
    }

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await repository.DeleteAsync(id, cancellationToken))
        {
            throw new KeyNotFoundException("Объект не найден");
        }
    }

    private static Service ToEntity(int id, SaveServiceRequest request)
    {
        var price = request.Price!.Value;
        if (decimal.Round(price, 2) != price)
        {
            throw new ArgumentException("Стоимость услуги указывается с точностью до копейки");
        }

        return new Service
        {
            Id = id,
            Name = request.Name.Trim(),
            Category = request.Category?.Trim(),
            Price = price,
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
