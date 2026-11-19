using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Specialists;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Application.Services;

/// <summary>
/// Операции с сущностями BeautySpecialist
/// </summary>
/// <param name="repository">Репозиторий сущностей</param>
public sealed class SpecialistService(ISpecialistRepository repository) : ISpecialistService
{
    /// <summary>
    /// Получить список объектов
    /// </summary>
    public IReadOnlyList<SpecialistResponse> GetAll()
    {
        return repository.GetAll().Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    public SpecialistResponse GetById(int id)
    {
        var entity = repository.GetById(id)
            ?? throw new KeyNotFoundException("Объект не найден");
        return ToResponse(entity);
    }

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    public SpecialistResponse Create(SaveSpecialistRequest request)
    {
        var entity = ToEntity(0, request);
        return ToResponse(repository.Add(entity));
    }

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    public void Update(int id, SaveSpecialistRequest request)
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

    private static BeautySpecialist ToEntity(int id, SaveSpecialistRequest request)
    {
        PersonRules.ValidateBirthDate(request.DateOfBirth!.Value);
        return new BeautySpecialist
        {
            Id = id,
            LastName = request.LastName.Trim(),
            FirstName = request.FirstName.Trim(),
            Patronymic = request.Patronymic?.Trim(),
            Gender = request.Gender!.Value,
            DateOfBirth = request.DateOfBirth!.Value,
            PhoneNumber = request.PhoneNumber?.Trim(),
            PassportNumber = request.PassportNumber?.Trim(),
            Specialization = request.Specialization?.Trim(),
            WorkExperience = request.WorkExperience!.Value,
        };
    }

    private static SpecialistResponse ToResponse(BeautySpecialist entity)
    {
        return new SpecialistResponse
        {
            Id = entity.Id,
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Patronymic = entity.Patronymic,
            Gender = entity.Gender,
            DateOfBirth = entity.DateOfBirth,
            PhoneNumber = entity.PhoneNumber,
            PassportNumber = entity.PassportNumber,
            Specialization = entity.Specialization,
            WorkExperience = entity.WorkExperience,
        };
    }
}