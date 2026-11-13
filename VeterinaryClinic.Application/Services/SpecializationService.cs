using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «специализация»
/// </summary>
public sealed class SpecializationService(IRepository<Specialization> specializations, IRepository<Veterinarian> veterinarians) : ISpecializationService
{
    /// <summary>
    /// Возвращает все специализации
    /// </summary>
    public IReadOnlyList<SpecializationDto> Specializations() => specializations.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public SpecializationDto? Get(int id) => specializations.Get(id) is { } s ? DtoMapper.Map(s) : null;

    /// <summary>
    /// Создаёт специализацию с уникальной парой названия и вида животных
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public SpecializationDto Create(SaveSpecializationRequest x)
    {
        Validate(x);
        EnsureUnique(x, null);
        return DtoMapper.Map(specializations.Create(id => new Specialization
        { Id = id, Name = x.Name.Trim(), AnimalType = x.AnimalType }));
    }

    /// <summary>
    /// Обновляет специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public SpecializationDto Update(int id, SaveSpecializationRequest x)
    {
        Validate(x);
        var current = specializations.Get(id) ?? throw new KeyNotFoundException($"Specialization {id} not found");
        EnsureUnique(x, id);
        if (current.AnimalType != x.AnimalType && veterinarians.Any(v => v.Specialization.Id == id))
            throw new InvalidOperationException("Cannot change species of a specialization in use");
        return DtoMapper.Map(specializations.Update(id, s => { s.Name = x.Name.Trim(); s.AnimalType = x.AnimalType; }));
    }

    /// <summary>
    /// Удаляет неиспользуемую специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteSpecialization(int id)
    {
        if (veterinarians.Any(v => v.Specialization.Id == id))
            throw new InvalidOperationException("Specialization is in use");
        specializations.Remove(id);
    }

    /// <summary>
    /// Возвращает ветеринаров по специализации
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecialization(int id)
    {
        if (specializations.Get(id) is null) throw new KeyNotFoundException($"Specialization {id} not found");
        return veterinarians.Find(v => v.Specialization.Id == id).Select(DtoMapper.Map).ToArray();
    }
    private static void Validate(SaveSpecializationRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType))
            throw new ArgumentException("Invalid specialization");
    }
    private void EnsureUnique(SaveSpecializationRequest x, int? exceptId)
    {
        if (specializations.Any(s => s.Id != exceptId && s.AnimalType == x.AnimalType &&
            string.Equals(s.Name, x.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Specialization already exists");
    }
}
