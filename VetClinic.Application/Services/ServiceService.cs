using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Services;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для управления перечнем медицинских услуг ветеринарной клиники
/// </summary>
/// <param name="serviceRepository">Репозиторий для работы с услугами</param>
/// <param name="specializationRepository">Репозиторий специализаций</param>
/// <param name="logger">Логгер</param>
public class ServiceAppService(IRepository<Service> serviceRepository, IRepository<Specialization> specializationRepository, ILogger<ServiceAppService> logger) : IServiceAppService
{
    ///<inheritdoc/>
    public async Task<List<ServiceDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение всех видов услуг");
        var list = await serviceRepository.GetAll();
        logger.LogInformation("Из репозитория успешно извлечено {Count} услуг", list.Count);
        return list.Select(x => new ServiceDto(x.Id, x.Title, x.Price, x.Specialization.Title)).ToList();
    }

    ///<inheritdoc/>
    public async Task<ServiceDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Получение услуги с Id {Id}", id);
        var x = await serviceRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Услуга с Id {Id} отсутствует в хранилище данных.", id);
            return null;
        }
        return x == null ? null : new ServiceDto(x.Id, x.Title, x.Price, x.Specialization.Title);
    }

    ///<inheritdoc/>
    public async Task<ServiceDto> Create(ServiceCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Запуск валидации связей для нового питомца '{Title}'.", dto.Title);
        var spec = await specializationRepository.Get(dto.SpecializationId) ?? throw new Exception("Специализация не найдена");
        var entity = new Service { Title = dto.Title, Price = dto.Price, SpecializationId = dto.SpecializationId, Specialization = spec };
        await serviceRepository.Create(entity);
        logger.LogInformation("Услуга успешно создана с названием {Title} и Id {Id}", entity.Title, entity.Id);
        return new ServiceDto(entity.Id, entity.Title, entity.Price, spec.Title);
    }

    ///<inheritdoc/>
    public async Task<ServiceDto> Update(ServiceCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление данных об услуге с Id {Id}", id);
        var spec = await specializationRepository.Get(dto.SpecializationId) ?? throw new Exception("Специализация не найдена");
        var entity = new Service { Id = id, Title = dto.Title, Price = dto.Price, SpecializationId = dto.SpecializationId, Specialization = spec };
        await serviceRepository.Update(entity);
        logger.LogInformation("Услуга с Id {id} успешно обновлена", id);
        return new ServiceDto(entity.Id, entity.Title, entity.Price, spec.Title);
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление данных об услуге с Id {Id}", id);
        var x = await serviceRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Отмена удаления: Услуга с Id {Id} не найдена.", id);
            return false;
        }
        await serviceRepository.Delete(id);
        logger.LogInformation("Услуга с Id {id} успешно удалена", id);
        return true;
    }
}
