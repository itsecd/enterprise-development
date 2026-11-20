using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «ветеринар»
/// </summary>
public sealed class VeterinarianService(IRepository<Veterinarian> veterinarians, IRepository<Specialization> specializations, IRepository<Appointment> appointments) : IVeterinarianService
{
    private static void Validate(SaveVeterinarianRequest x)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - x.BirthDate.Year;
        if (x.BirthDate > today.AddYears(-age)) age--;
        if (string.IsNullOrWhiteSpace(x.FirstName) || string.IsNullOrWhiteSpace(x.LastName) ||
            string.IsNullOrWhiteSpace(x.PassportNumber) || x.BirthDate == default ||
            x.BirthDate > today || age < 18 || x.ExperienceYears < 0 || x.ExperienceYears > age - 18)
            throw new ArgumentException("Некорректный возраст или стаж ветеринара");
    }

    public async Task<IReadOnlyList<VeterinarianDto>> VeterinariansAsync(CancellationToken cancellationToken = default) => (await veterinarians.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<VeterinarianDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await veterinarians.GetAsync(id, cancellationToken)) is { } v ? DtoMapper.Map(v) : null;

    public async Task<VeterinarianDto> CreateAsync(SaveVeterinarianRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var spec = (await specializations.GetAsync(x.SpecializationId, cancellationToken)) ?? throw new ArgumentException("Специализация не найдена");
        return DtoMapper.Map((await veterinarians.CreateAsync(new Veterinarian
        {
            Specialization = spec,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic,
            Phone = x.Phone,
            PassportNumber = x.PassportNumber,
            BirthDate = x.BirthDate,
            ExperienceYears = x.ExperienceYears
        }, cancellationToken)));
    }


    public async Task<VeterinarianDto> UpdateAsync(int id, SaveVeterinarianRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var spec = (await specializations.GetAsync(x.SpecializationId, cancellationToken)) ?? throw new ArgumentException("Специализация не найдена");
        if ((await appointments.AnyAsync(a => a.Veterinarian.Id == id && a.Pet.Breed.AnimalType != spec.AnimalType, cancellationToken)))
            throw new InvalidOperationException("Специализация несовместима с существующими приёмами");
        return DtoMapper.Map((await veterinarians.UpdateAsync(id, v =>
        {
            v.FirstName = x.FirstName;
            v.LastName = x.LastName;
            v.Patronymic = x.Patronymic;
            v.Phone = x.Phone;
            v.PassportNumber = x.PassportNumber;
            v.BirthDate = x.BirthDate;
            v.Specialization = spec;
            v.ExperienceYears = x.ExperienceYears;
        }, cancellationToken)));
    }


    public async Task DeleteVeterinarianAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await appointments.AnyAsync(a => a.Veterinarian.Id == id, cancellationToken)))
            throw new InvalidOperationException("У ветеринара есть записи на приём");
        await veterinarians.RemoveAsync(id, cancellationToken);
    }


    public async Task<IReadOnlyList<VeterinarianDto>> VeterinariansBySpeciesAsync(AnimalType species, CancellationToken cancellationToken = default) => (await veterinarians.FindAsync(v => v.Specialization.AnimalType == species, cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<IReadOnlyList<AppointmentDto>> AppointmentsByVeterinarianAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await veterinarians.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Ветеринар {id} не найден");
        return (await appointments.FindAsync(a => a.Veterinarian.Id == id, cancellationToken)).OrderBy(a => a.AppointmentDate)
            .Select(DtoMapper.Map).ToArray();
    }


    public async Task<IReadOnlyList<PetDto>> PetsByVeterinarianAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await veterinarians.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Ветеринар {id} не найден");
        return (await appointments.FindAsync(a => a.Veterinarian.Id == id, cancellationToken)).Select(a => a.Pet)
            .DistinctBy(p => p.Id).OrderBy(p => p.Name).Select(DtoMapper.Map).ToArray();
    }
}
