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
/// Выполняет операции с ресурсом «специализация»
/// </summary>
public sealed class SpecializationService(IRepository<Specialization> specializations, IRepository<Veterinarian> veterinarians) : ISpecializationService
{
    private static void Validate(SaveSpecializationRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType))
            throw new ArgumentException("Некорректные данные специализации");
    }
    public async Task<IReadOnlyList<SpecializationDto>> SpecializationsAsync(CancellationToken cancellationToken = default) => (await specializations.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<SpecializationDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await specializations.GetAsync(id, cancellationToken)) is { } s ? DtoMapper.Map(s) : null;

    public async Task<SpecializationDto> CreateAsync(SaveSpecializationRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        await EnsureUniqueAsync(x, null, cancellationToken);
        return DtoMapper.Map((await specializations.CreateAsync(new Specialization
        { Name = x.Name.Trim(), AnimalType = x.AnimalType }, cancellationToken)));
    }


    public async Task<SpecializationDto> UpdateAsync(int id, SaveSpecializationRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var current = (await specializations.GetAsync(id, cancellationToken)) ?? throw new KeyNotFoundException($"Специализация {id} не найдена");
        await EnsureUniqueAsync(x, id, cancellationToken);
        if (current.AnimalType != x.AnimalType && (await veterinarians.AnyAsync(v => v.Specialization.Id == id, cancellationToken)))
            throw new InvalidOperationException("Нельзя изменить вид используемой специализации");
        return DtoMapper.Map((await specializations.UpdateAsync(id, s => { s.Name = x.Name.Trim(); s.AnimalType = x.AnimalType; }, cancellationToken)));
    }


    public async Task DeleteSpecializationAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await veterinarians.AnyAsync(v => v.Specialization.Id == id, cancellationToken)))
            throw new InvalidOperationException("Специализация используется");
        await specializations.RemoveAsync(id, cancellationToken);
    }


    public async Task<IReadOnlyList<VeterinarianDto>> VeterinariansBySpecializationAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await specializations.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Специализация {id} не найдена");
        return (await veterinarians.FindAsync(v => v.Specialization.Id == id, cancellationToken)).Select(DtoMapper.Map).ToArray();
    }

    private async Task EnsureUniqueAsync(SaveSpecializationRequest x, int? exceptId, CancellationToken cancellationToken)
    {
        if (await specializations.AnyAsync(s => s.Id != exceptId && s.AnimalType == x.AnimalType &&
            s.Name.ToLower() == x.Name.Trim().ToLower(), cancellationToken))
            throw new InvalidOperationException("Специализация уже существует");
    }
}
