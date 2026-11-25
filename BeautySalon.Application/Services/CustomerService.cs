using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Customers;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Application.Services;

/// <summary>
/// Операции с сущностями Customer
/// </summary>
/// <param name="repository">Репозиторий сущностей</param>
public sealed class CustomerService(ICustomerRepository repository) : ICustomerService
{
    /// <summary>
    /// Получить список объектов
    /// </summary>
    public async Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await repository.GetAllAsync(cancellationToken)).Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public async Task<CustomerResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Объект не найден");
        return ToResponse(entity);
    }

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    public async Task<CustomerResponse> CreateAsync(SaveCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(0, request);
        return ToResponse(await repository.AddAsync(entity, cancellationToken));
    }

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    public async Task UpdateAsync(int id, SaveCustomerRequest request, CancellationToken cancellationToken = default)
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

    private static Customer ToEntity(int id, SaveCustomerRequest request)
    {
        PersonRules.ValidateBirthDate(request.DateOfBirth!.Value);
        return new Customer
        {
            Id = id,
            LastName = request.LastName.Trim(),
            FirstName = request.FirstName.Trim(),
            Patronymic = request.Patronymic?.Trim(),
            Gender = request.Gender!.Value,
            DateOfBirth = request.DateOfBirth!.Value,
            PhoneNumber = request.PhoneNumber?.Trim(),
        };
    }

    private static CustomerResponse ToResponse(Customer entity)
    {
        return new CustomerResponse
        {
            Id = entity.Id,
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Patronymic = entity.Patronymic,
            Gender = entity.Gender,
            DateOfBirth = entity.DateOfBirth,
            PhoneNumber = entity.PhoneNumber,
        };
    }
}
