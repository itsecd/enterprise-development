using Microsoft.Extensions.Logging;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления записями на прием
/// </summary>
/// <param name="appointmentRepository">Репозиторий приемов</param>
/// <param name="petRepository">Репозиторий питомцев</param>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
/// <param name="logger">Логгер сервиса</param>
public class AppointmentService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Pet, int> petRepository,
    IRepository<Veterinarian, int> veterinarianRepository,
    ILogger<AppointmentService> logger)
    : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<AppointmentDto>> GetAllAsync()
    {
        var appointments = await appointmentRepository.GetAllAsync();
        return [.. appointments.Select(a => a.ToDto())];
    }

    /// <inheritdoc />
    public async Task<AppointmentDto?> GetByIdAsync(int id)
    {
        var appointment = await appointmentRepository.GetByIdAsync(id);
        return appointment?.ToDto();
    }

    /// <inheritdoc />
    public async Task<AppointmentDto> CreateAsync(AppointmentCreateUpdateDto dto)
    {
        var pet = await petRepository.GetByIdAsync(dto.PetId);
        if (pet is null)
        {
            logger.LogWarning("Ошибка создания приема: Питомец с Id {PetId} не найден", dto.PetId);
            throw new KeyNotFoundException($"Питомец с Id {dto.PetId} не найден");
        }

        var vet = await veterinarianRepository.GetByIdAsync(dto.VeterinarianId);
        if (vet is null)
        {
            logger.LogWarning("Ошибка создания приема: Врач с Id {VetId} не найден", dto.VeterinarianId);
            throw new KeyNotFoundException($"Врач с Id {dto.VeterinarianId} не найден");
        }

        var appt = dto.ToEntity(pet, vet);
        var created = await appointmentRepository.AddAsync(appt);

        pet.Appointments.Add(created);
        vet.Appointments.Add(created);

        logger.LogInformation("Успешно создана запись на прием с Id {Id} (Питомец: {PetId}, Врач: {VetId})", created.Id, pet.Id, vet.Id);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, AppointmentCreateUpdateDto dto)
    {
        var existing = await appointmentRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующей записи на прием с Id {Id}", id);
            return false;
        }

        var pet = await petRepository.GetByIdAsync(dto.PetId);
        if (pet is null)
        {
            logger.LogWarning("Ошибка обновления приема {Id}: Питомец с Id {PetId} не найден", id, dto.PetId);
            throw new KeyNotFoundException($"Питомец с Id {dto.PetId} не найден");
        }

        var vet = await veterinarianRepository.GetByIdAsync(dto.VeterinarianId);
        if (vet is null)
        {
            logger.LogWarning("Ошибка обновления приема {Id}: Врач с Id {VetId} не найден", id, dto.VeterinarianId);
            throw new KeyNotFoundException($"Врач с Id {dto.VeterinarianId} не найден");
        }

        if (existing.Pet.Id != pet.Id)
        {
            existing.Pet.Appointments.Remove(existing);
            pet.Appointments.Add(existing);
        }

        if (existing.Veterinarian.Id != vet.Id)
        {
            existing.Veterinarian.Appointments.Remove(existing);
            vet.Appointments.Add(existing);
        }

        existing.Pet = pet;
        existing.Veterinarian = vet;
        existing.AppointmentDateTime = dto.AppointmentDateTime;
        existing.RoomNumber = dto.RoomNumber;
        existing.IsRepeat = dto.IsRepeat;

        var result = await appointmentRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Запись на прием с Id {Id} успешно обновлена", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await appointmentRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка удаления несуществующей записи на прием с Id {Id}", id);
            return false;
        }

        existing.Pet.Appointments.Remove(existing);
        existing.Veterinarian.Appointments.Remove(existing);

        var result = await appointmentRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Запись на прием с Id {Id} успешно удалена", id);
        }
        return result;
    }
} 