using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Owners;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class OwnerService(IOwnerRepository owners, ILogger<OwnerService> logger) : IOwnerService
{
    public async Task<IReadOnlyList<OwnerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка владельцев");
        var result = (await owners.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToOwner).ToArray();
        logger.LogInformation("Список владельцев получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<OwnerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение владельца {OwnerId}", id);
        var owner = await owners.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("владелец", id);
        logger.LogInformation("Владелец {OwnerId} получен", id);
        return ClinicMapper.ToOwner(owner);
    }

    public async Task<IReadOnlyList<OwnerDto>> GetWithSeveralPetsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение владельцев с несколькими питомцами");
        var result = (await owners.GetWithSeveralPetsAsync(cancellationToken)).Select(ClinicMapper.ToOwner).ToArray();
        logger.LogInformation("Владельцы с несколькими питомцами получены, записей: {Count}", result.Length);
        return result;
    }

    public async Task<OwnerDto> CreateAsync(OwnerEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание владельца");
        var created = await owners.AddAsync(ReadName(dto), ReadPhone(dto), DtoValidator.OptionalText(dto.Address), cancellationToken);
        logger.LogInformation("Владелец {OwnerId} создан", created.Id);
        return ClinicMapper.ToOwner(created);
    }

    public async Task<OwnerDto> UpdateAsync(int id, OwnerEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление владельца {OwnerId}", id);
        var updated = await owners.UpdateAsync(id, ReadName(dto), ReadPhone(dto), DtoValidator.OptionalText(dto.Address), cancellationToken);
        logger.LogInformation("Владелец {OwnerId} обновлён", id);
        return ClinicMapper.ToOwner(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление владельца {OwnerId}", id);
        await owners.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Владелец {OwnerId} удалён", id);
    }

    private static string ReadName(OwnerEditDto dto)
    {
        DtoValidator.EnsureValid(dto);
        return DtoValidator.RequireText(dto.FullName, "Укажите ФИО владельца.");
    }

    private static string ReadPhone(OwnerEditDto dto) =>
        DtoValidator.RequireText(dto.PhoneNumber, "Укажите телефон.");
}
