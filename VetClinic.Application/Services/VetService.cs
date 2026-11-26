using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Services;
/// <summary>
/// Прикладной сервис для управления составом ветеринаров
/// </summary>
/// <param name="vetRepository">Специализированный репозиторий для работы с врачами</param>
/// <param name="specializationRepository">Репозиторий специализаций</param>
/// <param name="logger">Логгер</param>

public class VetAppService(
    IVetRepository vetRepository,
    IRepository<Specialization> specializationRepository,
    ILogger<VetAppService> logger) : IVetAppService
{
    ///<inheritdoc/>
    public async Task<List<VetDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение списка всех ветеринарных врачей");
        var list = await vetRepository.GetAll();
        logger.LogInformation("Из репозитория успешно извлечено {Count} врачей. Начинается маппинг в DTO.", list.Count);
        return list.Select(x => new VetDto(x.Id, x.FullName, x.ExpirienceYears, x.Specialization.Title)).ToList();
    }

    ///<inheritdoc/>
    public async Task<VetDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Поиск врача с Id {Id}", id);
        var x = await vetRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Врач с Id {Id} отсутствует в хранилище данных.", id);
            return null;
        }
        return new VetDto(x.Id, x.FullName, x.ExpirienceYears, x.Specialization.Title);
    }

    ///<inheritdoc/>
    public async Task<VetDto> Create(VetCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Добавление нового врача '{FullName}'", dto.FullName);
        var spec = await specializationRepository.Get(dto.SpecializationId);
        if (spec == null)
        {
            logger.LogError("Ошибка создания: Специализация с Id {Id} не найдена.", dto.SpecializationId);
            throw new Exception("Специализация не найдена");
        }

        var entity = new Vet
        {
            FullName = dto.FullName,
            PassportNumber = dto.PassportNumber,
            BirthDate = dto.BirthDate,
            ExpirienceYears = dto.ExpirienceYears,
            SpecializationId = dto.SpecializationId,
            Specialization = spec
        };

        await vetRepository.Create(entity);
        logger.LogInformation("Врач '{FullName}' успешно добавлен в систему с присвоенным Id {Id}", entity.FullName, entity.Id);
        return new VetDto(entity.Id, entity.FullName, entity.ExpirienceYears, spec.Title);
    }

    ///<inheritdoc/>
    public async Task<VetDto> Update(VetCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление данных врача с Id {Id}", id);
        var spec = await specializationRepository.Get(dto.SpecializationId);
        if (spec == null)
        {
            logger.LogError("Ошибка обновления: Специализация с Id {Id} не найдена для врача с Id {VetId}.", dto.SpecializationId, id);
            throw new Exception("Специализация не найдена");
        }

        var entity = new Vet
        {
            Id = id,
            FullName = dto.FullName,
            PassportNumber = dto.PassportNumber,
            BirthDate = dto.BirthDate,
            ExpirienceYears = dto.ExpirienceYears,
            SpecializationId = dto.SpecializationId,
            Specialization = spec
        };

        await vetRepository.Update(entity);
        logger.LogInformation("Профиль врача с Id {Id} успешно обновлен в базе данных.", id);
        return new VetDto(entity.Id, entity.FullName, entity.ExpirienceYears, spec.Title);
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление профиля врача с Id {Id}", id);
        var exists = await vetRepository.Get(id);
        if (exists == null)
        {
            logger.LogWarning("Отмена удаления: Врач с Id {Id} не найден в системе.", id);
            return false;
        }

        await vetRepository.Delete(id);
        logger.LogInformation("Врач с Id {Id} успешно удален из кадровой базы данных.", id);
        return true;
    }

    ///<inheritdoc/>
    public async Task<List<VetDto>> GetVetsBySpecies(Species species)
    {
        logger.LogInformation("Аналитический запрос: Фильтрация врачей по виду животных '{Species}'", species);
        var list = await vetRepository.FindVetsBySpecies(species);
        logger.LogInformation("Фильтрация завершена: найдено {Count} специалистов по виду '{Species}'.", list.Count, species);
        return list.Select(x => new VetDto(x.Id, x.FullName, x.ExpirienceYears, x.Specialization.Title)).ToList();
    }
}
