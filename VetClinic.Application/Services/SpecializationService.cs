using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Specializations;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для управления справочником врачебных специализаций
/// </summary>
/// <param name="specializationRepository">Репозиторий специализаций </param>
/// <param name="logger">Логгер</param>
public class SpecializationAppService(IRepository<Specialization> specializationRepository, ILogger<SpecializationAppService> logger) : ISpecializationAppService
{
    ///<inheritdoc/>
    public async Task<List<SpecializationDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение всех специализаций врачей");
        var list = await specializationRepository.GetAll();
        logger.LogInformation("Из репозитория извлечено {Count} специализаций. Начинается маппинг в DTO.", list.Count);
        return list.Select(x => new SpecializationDto(x.Id, x.Title, x.TargetSpecies.ToString())).ToList();
    }

    ///<inheritdoc/>
    public async Task<SpecializationDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Получение специализации с Id {Id}", id);
        var x = await specializationRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена в хранилище данных.", id);
            return null;
        }
        return new SpecializationDto(x.Id, x.Title, x.TargetSpecies.ToString());
    }

    ///<inheritdoc/>
    public async Task<SpecializationDto> Create(SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Создание специализации с названием '{Title}'", dto.Title);
        var entity = new Specialization { Title = dto.Title, TargetSpecies = dto.TargetSpecies };
        await specializationRepository.Create(entity);
        logger.LogInformation("Специализация '{Title}' успешно создана с Id {Id}", entity.Title, entity.Id);
        return new SpecializationDto(entity.Id, entity.Title, entity.TargetSpecies.ToString());
    }

    ///<inheritdoc/>
    public async Task<SpecializationDto> Update(SpecializationCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление данных специализации с Id {Id}", id);
        var entity = new Specialization { Id = id, Title = dto.Title, TargetSpecies = dto.TargetSpecies };
        await specializationRepository.Update(entity);
        logger.LogInformation("Специализация с Id {Id} успешно обновлена", id);
        return new SpecializationDto(entity.Id, entity.Title, entity.TargetSpecies.ToString());
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление специализации с Id {Id}", id);
        var exists = await specializationRepository.Get(id);
        if (exists == null)
        {
            logger.LogWarning("Отмена удаления: Специализация с Id {Id} не найдена.", id);
            return false;
        }

        await specializationRepository.Delete(id);
        logger.LogInformation("Специализация с Id {Id} успешно удалена", id);
        return true;
    }
}

