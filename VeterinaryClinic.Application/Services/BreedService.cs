using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «порода»
/// </summary>
public sealed class BreedService(IRepository<Breed> breeds, IRepository<Pet> pets, IRepository<Appointment> appointments) : IBreedService
{
    /// <summary>
    /// Возвращает все породы
    /// </summary>
    public IReadOnlyList<BreedDto> Breeds() => breeds.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public BreedDto? Get(int id) => breeds.Get(id) is { } b ? DtoMapper.Map(b) : null;

    /// <summary>
    /// Создаёт породу
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public BreedDto Create(SaveBreedRequest x)
    {
        Validate(x);
        return DtoMapper.Map(breeds.Create(id => new Breed { Id = id, Name = x.Name.Trim(), AnimalType = x.AnimalType }));
    }

    /// <summary>
    /// Изменяет породу, если она не используется с другим видом животных
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public BreedDto Update(int id, SaveBreedRequest x)
    {
        Validate(x);
        var current = breeds.Get(id) ?? throw new KeyNotFoundException($"Breed {id} not found");
        if (current.AnimalType != x.AnimalType && pets.Any(p => p.Breed.Id == id))
            throw new InvalidOperationException("Cannot change species of a breed used by pets");
        return DtoMapper.Map(breeds.Update(id, b => { b.Name = x.Name.Trim(); b.AnimalType = x.AnimalType; }));
    }

    /// <summary>
    /// Удаляет неиспользуемую породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteBreed(int id)
    {
        if (pets.Any(p => p.Breed.Id == id)) throw new InvalidOperationException("Breed is in use");
        breeds.Remove(id);
    }

    /// <summary>
    /// Считает повторные приёмы для породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    public int RepeatedAppointments(int breedId)
    {
        if (breeds.Get(breedId) is null) throw new KeyNotFoundException($"Breed {breedId} not found");
        return appointments.Count(a => a.IsRepeated && a.Pet.Breed.Id == breedId);
    }

    /// <summary>
    /// Возвращает питомцев выбранной породы
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByBreed(int id)
    {
        if (breeds.Get(id) is null) throw new KeyNotFoundException($"Breed {id} not found");
        return pets.Find(p => p.Breed.Id == id).Select(DtoMapper.Map).ToArray();
    }
    private static void Validate(SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType))
            throw new ArgumentException("Invalid breed");
    }
}
