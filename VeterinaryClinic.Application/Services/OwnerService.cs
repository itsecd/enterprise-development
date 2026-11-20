using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «владелец»
/// </summary>
public sealed class OwnerService(IRepository<Owner> owners, IRepository<Pet> pets, IOwnerAnalytics analytics) : IOwnerService
{
    private static void Validate(SaveOwnerRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.Phone) || string.IsNullOrWhiteSpace(x.Address))
            throw new ArgumentException("Необходимо заполнить данные владельца");
    }

    public async Task<IReadOnlyList<OwnerDto>> OwnersAsync(CancellationToken cancellationToken = default) => (await owners.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<OwnerDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await owners.GetAsync(id, cancellationToken)) is { } o ? DtoMapper.Map(o) : null;

    public async Task<OwnerDto> CreateAsync(SaveOwnerRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await owners.CreateAsync(new Owner
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic,
            Phone = x.Phone,
            Address = x.Address
        }, cancellationToken)));
    }


    public async Task<OwnerDto> UpdateAsync(int id, SaveOwnerRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await owners.UpdateAsync(id, o =>
        {
            o.FirstName = x.FirstName; o.LastName = x.LastName;
            o.Patronymic = x.Patronymic; o.Phone = x.Phone; o.Address = x.Address;
        }, cancellationToken)));
    }


    public async Task DeleteOwnerAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var owner = await owners.GetAsync(id, cancellationToken);

        if (owner is null)
            return;

        await owners.RemoveAsync(id, cancellationToken);

        foreach (var pet in owner.Pets.ToArray())
        {
            pet.ChangeOwner(null);
        }
    }


    public async Task<IReadOnlyList<OwnerDto>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default)
        => (await analytics.OwnersWithMultiplePetsAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<IReadOnlyList<PetDto>> PetsByOwnerAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await owners.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Владелец {id} не найден");
        return (await pets.FindAsync(p => p.Owner != null && p.Owner.Id == id, cancellationToken)).Select(DtoMapper.Map).ToArray();
    }
}
