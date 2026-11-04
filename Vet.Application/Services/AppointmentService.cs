using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления записями на приём
/// </summary>
/// <param name="appointmentRepository">Репозиторий записей на прием</param>
/// <param name="petRepository">Репозиторий питомцев</param>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
public class AppointmentService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Pet, int> petRepository,
    IRepository<Veterinarian, int> veterinarianRepository)
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
        var pet = await petRepository.GetByIdAsync(dto.PetId)
            ?? throw new KeyNotFoundException($"Питомец с Id {dto.PetId} не найден");

        var vet = await veterinarianRepository.GetByIdAsync(dto.VeterinarianId)
            ?? throw new KeyNotFoundException($"Врач с Id {dto.VeterinarianId} не найден");

        var appt = dto.ToEntity(pet, vet);
        var created = await appointmentRepository.AddAsync(appt);

        pet.Appointments.Add(created);
        vet.Appointments.Add(created);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, AppointmentCreateUpdateDto dto)
    {
        var existing = await appointmentRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        var pet = await petRepository.GetByIdAsync(dto.PetId)
            ?? throw new KeyNotFoundException($"Питомец с Id {dto.PetId} не найден");

        var vet = await veterinarianRepository.GetByIdAsync(dto.VeterinarianId)
            ?? throw new KeyNotFoundException($"Врач с Id {dto.VeterinarianId} не найден");

        existing.Pet = pet;
        existing.Veterinarian = vet;
        existing.AppointmentDateTime = dto.AppointmentDateTime;
        existing.RoomNumber = dto.RoomNumber;
        existing.IsRepeat = dto.IsRepeat;

        return await appointmentRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await appointmentRepository.DeleteAsync(id);
    }
}