using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «владелец»
/// </summary>
public sealed class OwnerService(IRepository<Owner> owners, IRepository<Pet> pets) : IOwnerService
{
    /// <summary>
    /// Возвращает всех владельцев
    /// </summary>
    public IReadOnlyList<OwnerDto> Owners() => owners.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public OwnerDto? Get(int id) => owners.Get(id) is { } o ? DtoMapper.Map(o) : null;

    /// <summary>
    /// Создаёт владельца
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public OwnerDto Create(SaveOwnerRequest x)
    {
        Validate(x);
        return DtoMapper.Map(owners.Create(id => new Owner
        {
            Id = id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic,
            Phone = x.Phone,
            Address = x.Address
        }));
    }

    /// <summary>
    /// Обновляет сведения о владельце
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public OwnerDto Update(int id, SaveOwnerRequest x)
    {
        Validate(x);
        return DtoMapper.Map(owners.Update(id, o =>
        {
            o.FirstName = x.FirstName; o.LastName = x.LastName;
            o.Patronymic = x.Patronymic; o.Phone = x.Phone; o.Address = x.Address;
        }));
    }

    /// <summary>
    /// Удаляет владельца и снимает связи с питомцами
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteOwner(int id)
    {
        var owner = owners.Get(id);
        if (owner is null) return;
        foreach (var pet in pets.Find(p => p.Owner?.Id == id)) pet.ChangeOwner(null);
        owner.Pets.Clear();
        owners.Remove(id);
    }

    /// <summary>
    /// Возвращает владельцев нескольких питомцев
    /// </summary>
    public IReadOnlyList<OwnerDto> OwnersWithMultiplePets() => owners.Find(o =>
        pets.Count(p => p.Owner?.Id == o.Id) > 1).OrderBy(o => o.LastName)
        .ThenBy(o => o.FirstName).ThenBy(o => o.Patronymic).Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Возвращает питомцев владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByOwner(int id)
    {
        if (owners.Get(id) is null) throw new KeyNotFoundException($"Owner {id} not found");
        return pets.Find(p => p.Owner?.Id == id).Select(DtoMapper.Map).ToArray();
    }
    private static void Validate(SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address))
            throw new ArgumentException("Owner fields are required");
    }
}
