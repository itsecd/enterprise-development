using Microsoft.Extensions.Logging;
using VetClinic.Application.Contracts.Pets;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

/// <summary>
/// Прикладной сервис для ведения картотеки питомцев клиники
/// </summary>
/// <param name="petRepository">Репозиторий для работы с питомцами</param>
/// <param name="breedRepository">Репозиторий пород</param>
/// <param name="ownerRepository">Репозиторий владельцев</param>
/// <param name="logger">Логгер</param>
public class PetAppService(
    IRepository<Pet> petRepository,
    IRepository<Breed> breedRepository,
    IRepository<Owner> ownerRepository,
    ILogger<PetAppService> logger) : IPetAppService
{
    ///<inheritdoc/>
    public async Task<List<PetDto>> GetAll()
    {
        logger.LogInformation("Запрос: Получение всех питомцев");
        var list = await petRepository.GetAll();
        logger.LogInformation("Из репозитория успешно извлечено {Count} питомцев", list.Count);
        return list.Select(x => new PetDto(x.Id, x.Name, x.Type.ToString(), x.Breed?.Title ?? "-", x.Owner.FullName, x.Weight ?? 0)).ToList();
    }

    ///<inheritdoc/>
    public async Task<PetDto?> Get(int id)
    {
        logger.LogInformation("Запрос: Поиск питомца с Id {Id}", id);
        var x = await petRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Питомец с Id {Id} отсутствует в хранилище данных", id);
            return null;
        }
        return new PetDto(x.Id, x.Name, x.Type.ToString(), x.Breed?.Title ?? "-", x.Owner.FullName, x.Weight ?? 0);
    }

    ///<inheritdoc/>
    public async Task<PetDto> Create(PetCreateUpdateDto dto)
    {
        logger.LogInformation("Запрос: Запуск валидации связей для нового питомца '{Name}'", dto.Name);
        var breed = await breedRepository.Get(dto.BreedId) ?? throw new KeyNotFoundException("Порода не найдена");
        var owner = await ownerRepository.Get(dto.OwnerId) ?? throw new KeyNotFoundException("Хозяин не найден");

        var entity = new Pet
        {
            Name = dto.Name,
            Type = dto.Type,
            BirthDate = dto.BirthDate,
            Weight = dto.Weight,
            BreedId = dto.BreedId,
            Breed = breed,
            OwnerId = dto.OwnerId,
            Owner = owner
        };

        await petRepository.Create(entity);
        logger.LogInformation("Питомец '{Name}' успешно сохранен с присвоенным Id {Id}", entity.Name, entity.Id);
        return new PetDto(entity.Id, entity.Name, entity.Type.ToString(), breed.Title, owner.FullName, entity.Weight ?? 0);
    }
    ///<inheritdoc/>
    public async Task<PetDto> Update(PetCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Запрос: Обновление данных питомца с Id {Id}", id);
        var breed = await breedRepository.Get(dto.BreedId) ?? throw new KeyNotFoundException("Порода не найдена");
        var owner = await ownerRepository.Get(dto.OwnerId) ?? throw new KeyNotFoundException("Хозяин не найден");

        var entity = new Pet
        {
            Id = id,
            Name = dto.Name,
            Type = dto.Type,
            BirthDate = dto.BirthDate,
            Weight = dto.Weight,
            BreedId = dto.BreedId,
            Breed = breed,
            OwnerId = dto.OwnerId,
            Owner = owner
        };
        await petRepository.Update(entity);
        logger.LogInformation("Данные питомца с Id {Id} успешно изменены", id);
        return new PetDto(entity.Id, entity.Name, entity.Type.ToString(), breed.Title, owner.FullName, entity.Weight ?? 0);
    }
    ///<inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Запрос: Удаление данных о питомце с Id {Id}.", id);
        var x = await petRepository.Get(id);
        if (x == null)
        {
            logger.LogWarning("Отмена удаления: Питомец с Id {Id} не найден", id);
            return false;
        }
        await petRepository.Delete(id);
        logger.LogInformation("Питомец с Id {Id} успешно удален из системы", id);
        return true;
    }
    ///<inheritdoc/>
    public async Task<List<PetDto>> GetSortedByName()
    {
        logger.LogInformation("Запрос: Выполнение внутренней сортировки питомцев по алфавиту");
        var list = await GetAll();
        return list.OrderBy(p => p.Name).ToList();
    }
}
