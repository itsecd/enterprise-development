using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления владельцами
/// </summary>
/// <param name="ownerRepository">Репозиторий владельцев</param>
public class OwnerService(IRepository<Owner, int> ownerRepository)
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
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, OwnerCreateUpdateDto dto)
    {
        var existing = await ownerRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        existing.FullName = dto.FullName;
        existing.Address = dto.Address;
        existing.PhoneNumber = dto.PhoneNumber;

        return await ownerRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await ownerRepository.DeleteAsync(id);
    }
}