using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Owners;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для управления профилями владельцев животных
/// </summary>
/// <param name="ownerRepository">Репозиторий для работы с данными владельцев </param>
/// <param name="logger">Логгер </param>
public class OwnerAppService(IRepository<Owner> ownerRepository, ILogger<OwnerAppService> logger) : IOwnerAppService
{
    ///<inheritdoc/>
    public async Task<List<OwnerDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение списка всех владельцев");
        var list = await ownerRepository.GetAll();
        logger.LogInformation("Из репозитория извлечено {Count} владельцев.", list.Count);
        return list.Select(x => new OwnerDto(x.Id, x.FullName, x.Phone, x.Adress)).ToList();
    }

    ///<inheritdoc/>
    public async Task<OwnerDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Получение данных владельца с Id {Id}", id);
        var x = await ownerRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Владелец с Id {Id} не найден.", id);
            return null;
        }
        return new OwnerDto(x.Id, x.FullName, x.Phone, x.Adress);
    }

    ///<inheritdoc/>
    public async Task<OwnerDto> Create(OwnerCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Регистрация нового владельца '{FullName}'", dto.FullName);
        var entity = new Owner { FullName = dto.FullName, Phone = dto.Phone, Adress = dto.Adress };
        await ownerRepository.Create(entity);
        logger.LogInformation("Владелец успешно сохранен с Id {Id}", entity.Id);
        return new OwnerDto(entity.Id, entity.FullName, entity.Phone, entity.Adress);
    }

    ///<inheritdoc/>
    public async Task<OwnerDto> Update(OwnerCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление профиля владельца с Id {Id}", id);
        var entity = new Owner { Id = id, FullName = dto.FullName, Phone = dto.Phone, Adress = dto.Adress };
        await ownerRepository.Update(entity);
        logger.LogInformation("Данные владельца с Id {Id} успешно изменены", id);
        return new OwnerDto(entity.Id, entity.FullName, entity.Phone, entity.Adress);
    }

    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление владельца с Id {Id}", id);
        var exists = await ownerRepository.Get(id);
        if (exists == null)
        {
            logger.LogWarning("Отмена удаления: Владелец с Id {Id} не найден.", id);
            return false;
        }
        await ownerRepository.Delete(id);
        logger.LogInformation("Профиль владельца с Id {Id} удален", id);
        return true;
    }
}
