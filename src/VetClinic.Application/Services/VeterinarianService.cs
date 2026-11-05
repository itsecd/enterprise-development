using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Veterinarians;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class VeterinarianService(
    IVeterinarianRepository veterinarians,
    TimeProvider time,
    ILogger<VeterinarianService> logger) : IVeterinarianService
{
    public async Task<IReadOnlyList<VeterinarianDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка ветеринаров");
        var result = (await veterinarians.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToVeterinarian).ToArray();
        logger.LogInformation("Список ветеринаров получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<VeterinarianDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение ветеринара {VeterinarianId}", id);
        var veterinarian = await veterinarians.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("ветеринар", id);
        logger.LogInformation("Ветеринар {VeterinarianId} получен", id);
        return ClinicMapper.ToVeterinarian(veterinarian);
    }

    public async Task<IReadOnlyList<VeterinarianDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение ветеринаров вида {Species}", species);
        var selected = DtoValidator.RequireSpecies(species);
        var result = (await veterinarians.GetBySpeciesAsync(selected, cancellationToken)).Select(ClinicMapper.ToVeterinarian).ToArray();
        logger.LogInformation("Ветеринары вида {Species} получены, записей: {Count}", selected, result.Length);
        return result;
    }

    public async Task<IReadOnlyList<VeterinarianDto>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение ветеринаров специализации {SpecializationId}", specializationId);
        var result = (await veterinarians.GetBySpecializationAsync(specializationId, cancellationToken)).Select(ClinicMapper.ToVeterinarian).ToArray();
        logger.LogInformation(
            "Ветеринары специализации {SpecializationId} получены, записей: {Count}",
            specializationId,
            result.Length);
        return result;
    }

    public async Task<VeterinarianDto> CreateAsync(VeterinarianEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание ветеринара");
        var values = Read(dto);
        var created = await veterinarians.AddAsync(
            values.PassportNumber,
            values.FullName,
            values.SpecializationId,
            values.BirthYear,
            values.ExperienceYears, cancellationToken);
        logger.LogInformation("Ветеринар {VeterinarianId} создан", created.Id);
        return ClinicMapper.ToVeterinarian(created);
    }

    public async Task<VeterinarianDto> UpdateAsync(int id, VeterinarianEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление ветеринара {VeterinarianId}", id);
        var values = Read(dto);
        var updated = await veterinarians.UpdateAsync(
            id,
            values.PassportNumber,
            values.FullName,
            values.SpecializationId,
            values.BirthYear,
            values.ExperienceYears, cancellationToken);
        logger.LogInformation("Ветеринар {VeterinarianId} обновлён", id);
        return ClinicMapper.ToVeterinarian(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление ветеринара {VeterinarianId}", id);
        await veterinarians.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Ветеринар {VeterinarianId} удалён", id);
    }

    private (string PassportNumber, string FullName, int SpecializationId, int BirthYear, int ExperienceYears) Read(
        VeterinarianEditDto dto)
    {
        DtoValidator.EnsureValid(dto);
        var birthYear = dto.BirthYear ?? throw new DomainRuleException("Укажите год рождения.");
        var experienceYears = dto.ExperienceYears ?? throw new DomainRuleException("Укажите стаж.");
        var age = time.GetLocalNow().Year - birthYear;
        if (age < 18)
        {
            throw new DomainRuleException("Год рождения врача указан некорректно.");
        }

        if (experienceYears > age)
        {
            throw new DomainRuleException("Стаж не может превышать возраст.");
        }

        return (
            DtoValidator.RequireText(dto.PassportNumber, "Укажите номер паспорта."),
            DtoValidator.RequireText(dto.FullName, "Укажите ФИО врача."),
            DtoValidator.RequireId(dto.SpecializationId, "Укажите специализацию."),
            birthYear,
            experienceYears);
    }
}
