using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Breeds;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для управления справочником пород животных
/// </summary>
/// <param name="breedRepository">Репозиторий для работы с породами животных </param>
/// <param name="logger">Логгер </param>
public class BreedAppService(IRepository<Breed> breedRepository, ILogger<BreedAppService> logger) : IBreedAppService
{
    ///<inheritdoc/>
    public async Task<List<BreedDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение всех пород животных");
        var list = await breedRepository.GetAll();
        logger.LogInformation("Из репозитория извлечено {Count} пород. Начинается маппинг.", list.Count);
        return list.Select(x => new BreedDto(x.Id, x.Title, x.Type.ToString())).ToList();
    }

    ///<inheritdoc/>
    public async Task<BreedDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Получение породы с Id {Id}", id);
        var x = await breedRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Порода с Id {Id} не найдена.", id);
            return null;
        }
        return new BreedDto(x.Id, x.Title, x.Type.ToString());
    }

    ///<inheritdoc/>
    public async Task<BreedDto> Create(BreedCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Добавление новой породы '{Title}'", dto.Title);
        var entity = new Breed { Title = dto.Title, Type = dto.TargetSpecies };
        await breedRepository.Create(entity);
        logger.LogInformation("Порода '{Title}' успешно добавлена с Id {Id}", entity.Title, entity.Id);
        return new BreedDto(entity.Id, entity.Title, entity.Type.ToString());
    }

    public async Task<BreedDto> Update(BreedCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление породы с Id {Id}", id);
        var entity = new Breed { Id = id, Title = dto.Title, Type = dto.TargetSpecies };
        await breedRepository.Update(entity);
        logger.LogInformation("Порода с Id {Id} успешно обновлена", id);
        return new BreedDto(entity.Id, entity.Title, entity.Type.ToString());
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление породы с Id {Id}", id);
        var exists = await breedRepository.Get(id);
        if (exists == null)
        {
            logger.LogWarning("Отмена удаления: Порода с Id {Id} не найдена.", id);
            return false;
        }
        await breedRepository.Delete(id);
        logger.LogInformation("Порода с Id {Id} успешно удалена", id);
        return true;
    }
}
