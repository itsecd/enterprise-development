using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба для CRUD операций c мастерами
/// </summary>
/// <param name="masterRepository">Репозиторий мастеров</param>
public class MasterService(IRepository<Master, int> masterRepository) : IApplicationService<MasterDto, MasterCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<MasterDto> Create(MasterCreateUpdateDto dto)
    {
        var client = Mapper.Map(dto);

        var result = await masterRepository.Create(client);

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await masterRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<MasterDto?> Get(int id)
    {
        var master = await masterRepository.Read(id);

        if (master == null)
            return null;

        return Mapper.Map(master);
    }

    /// <inheritdoc/>
    public async Task<IList<MasterDto>> GetAll() => Mapper.Map(await masterRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<MasterDto> Update(MasterCreateUpdateDto dto, int id)
    {
        var master = Mapper.Map(dto);
        master.Id = id;

        var result = await masterRepository.Update(master);
        return Mapper.Map(result);
    }
}
