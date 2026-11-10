using Microsoft.Extensions.Logging;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления ветеринарами
/// </summary>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
/// <param name="specializationRepository">Репозиторий специализаций</param>
/// <param name="appointmentRepository">Репозиторий приемов</param>
/// <param name="logger">Логгер сервиса</param>
public class VeterinarianService(
    IRepository<Veterinarian, int> veterinarianRepository,
    IRepository<Specialization, int> specializationRepository,
    IRepository<Appointment, int> appointmentRepository,
    ILogger<VeterinarianService> logger)
    : IApplicationService<VeterinarianDto, VeterinarianCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<VeterinarianDto>> GetAllAsync()
    {
        var vets = await veterinarianRepository.GetAllAsync();
        return [.. vets.Select(v => v.ToDto())];
    }

    /// <inheritdoc />
    public async Task<VeterinarianDto?> GetByIdAsync(int id)
    {
        var vet = await veterinarianRepository.GetByIdAsync(id);
        return vet?.ToDto();
    }

    /// <inheritdoc />
    public async Task<VeterinarianDto> CreateAsync(VeterinarianCreateUpdateDto dto)
    {
        var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId);
        if (spec is null)
        {
            logger.LogWarning("Ошибка создания врача: Специализация с Id {SpecId} не найдена", dto.SpecializationId);
            throw new KeyNotFoundException($"Специализация с Id {dto.SpecializationId} не найдена");
        }

        var vet = dto.ToEntity(spec);
        var created = await veterinarianRepository.AddAsync(vet);
        logger.LogInformation("Создан новый ветеринар с Id {Id} ({FullName})", created.Id, created.FullName);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, VeterinarianCreateUpdateDto dto)
    {
        var existing = await veterinarianRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующего ветеринара с Id {Id}", id);
            return false;
        }

        var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId);
        if (spec is null)
        {
            logger.LogWarning("Ошибка обновления врача {Id}: Специализация с Id {SpecId} не найдена", id, dto.SpecializationId);
            throw new KeyNotFoundException($"Специализация с Id {dto.SpecializationId} не найдена");
        }

        existing.PassportNumber = dto.PassportNumber;
        existing.FullName = dto.FullName;
        existing.Specialization = spec;
        existing.BirthYear = dto.BirthYear;
        existing.WorkExperience = dto.WorkExperience;

        var result = await veterinarianRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Ветеринар с Id {Id} успешно обновлен", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await veterinarianRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка удаления несуществующего ветеринара с Id {Id}", id);
            return false;
        }

        var allAppointments = await appointmentRepository.GetAllAsync();
        var vetAppointments = allAppointments.Where(a => a.Veterinarian.Id == id).ToList();

        foreach (var appt in vetAppointments)
        {
            appt.Pet.Appointments.Remove(appt);
            await appointmentRepository.DeleteAsync(appt.Id);
        }

        var result = await veterinarianRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Ветеринар с Id {Id} и все его приёмы ({ApptCount} шт.) успешно удалены", id, vetAppointments.Count);
        }
        return result;
    }
}