using VetClinic.Application.Exceptions;
using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис пород, при удалении породы у питомцев она сбрасывается на стороне базы
/// </summary>
/// <param name="breeds">Репозиторий пород</param>
/// <param name="pets">Репозиторий питомцев</param>
/// <param name="transaction">Общая транзакция проверки питомцев и сохранения породы</param>
public class BreedService(
    IBreedRepository breeds,
    IPetRepository pets,
    IClinicTransaction transaction) : IBreedService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<BreedDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await breeds.GetAllAsync(cancellationToken);
        return [.. all.Select(b => b.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<BreedDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await breeds.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<BreedDto> CreateAsync(BreedEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await breeds.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public Task<BreedDto?> UpdateAsync(int id, BreedEditDto dto, CancellationToken cancellationToken = default) =>
        transaction.ExecuteSerializableAsync<BreedDto?>(async token =>
        {
            var entity = await breeds.GetByIdAsync(id, token);
            if (entity is null)
            {
                return null;
            }

            if (entity.Species != dto.Species && await pets.AnyByBreedAsync(id, token))
            {
                throw new ConflictException($"Вид породы «{entity.Name}» нельзя изменить: к ней привязаны питомцы");
            }

            entity.Apply(dto);
            await breeds.SaveChangesAsync(token);
            return entity.ToDto();
        }, cancellationToken);

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await breeds.DeleteAsync(id, cancellationToken);
}
