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
    public IReadOnlyList<CustomerResponse> GetAll()
    {
        return repository.GetAll().Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public CustomerResponse GetById(int id)
    {
        var entity = repository.GetById(id)
            ?? throw new KeyNotFoundException("Объект не найден");
        return ToResponse(entity);
    }

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    public CustomerResponse Create(SaveCustomerRequest request)
    {
        var entity = ToEntity(0, request);
        return ToResponse(repository.Add(entity));
    }

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    public void Update(int id, SaveCustomerRequest request)
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