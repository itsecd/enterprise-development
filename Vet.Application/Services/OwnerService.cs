using Microsoft.Extensions.Logging;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления владельцами
/// </summary>
/// <param name="ownerRepository">Репозиторий владельцев</param>
/// <param name="petRepository">Репозиторий питомцев</param>
/// <param name="appointmentRepository">Репозиторий приемов</param>
/// <param name="logger">Логгер сервиса</param>
public class OwnerService(
    IRepository<Owner, int> ownerRepository,
    IRepository<Pet, int> petRepository,
    IRepository<Appointment, int> appointmentRepository,
    ILogger<OwnerService> logger)
    : IApplicationService<OwnerDto, OwnerCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<OwnerDto>> GetAllAsync()
    {
        var owners = await ownerRepository.GetAllAsync();
        return [.. owners.Select(o => o.ToDto())];
    }

    /// <inheritdoc />
    public async Task<OwnerDto?> GetByIdAsync(int id)
    {
        var owner = await ownerRepository.GetByIdAsync(id);
        return owner?.ToDto();
    }

    /// <inheritdoc />
    public async Task<OwnerDto> CreateAsync(OwnerCreateUpdateDto dto)
    {
        var owner = dto.ToEntity();
        var created = await ownerRepository.AddAsync(owner);
        logger.LogInformation("Создан новый владелец с Id {Id} ({FullName})", created.Id, created.FullName);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, OwnerCreateUpdateDto dto)
    {
        var existing = await ownerRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующего владельца с Id {Id}", id);
            return false;
        }

        existing.FullName = dto.FullName;
        existing.Address = dto.Address;
        existing.PhoneNumber = dto.PhoneNumber;

        var result = await ownerRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Владелец с Id {Id} успешно обновлен", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await ownerRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка удаления несуществующего владельца с Id {Id}", id);
            return false;
        }

        var allPets = await petRepository.GetAllAsync();
        var ownerPets = allPets.Where(p => p.Owner.Id == id).ToList();

        var allAppointments = await appointmentRepository.GetAllAsync();

        foreach (var pet in ownerPets)
        {
            var petAppointments = allAppointments.Where(a => a.Pet.Id == pet.Id).ToList();
            foreach (var appt in petAppointments)
            {
                appt.Veterinarian.Appointments.Remove(appt);
                await appointmentRepository.DeleteAsync(appt.Id);
            }

            await petRepository.DeleteAsync(pet.Id);
        }

        var result = await ownerRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Владелец с Id {Id}, его питомцы ({PetCount} шт.) и все их приёмы успешно удалены", id, ownerPets.Count);
        }
        return result;
    }
}