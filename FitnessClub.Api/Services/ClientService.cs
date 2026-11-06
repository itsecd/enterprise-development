
using FitnessClub.Api.Dtos.Clients;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Api.Services;

/// <summary>
/// Сервис для работы с клиентами фитнес-клуба.
/// </summary>
public class ClientService(IRepository<Client> repository)
{
    /// <summary>
    /// Получить всех клиентов.
    /// </summary>
    public IReadOnlyList<ClientDto> GetAll() => [.. repository.GetAll().Select(ToDto)];

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    public ClientDto? GetById(int id)
    {
        var client = repository.GetById(id);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Получить клиентов с просроченными абонементами.
    /// </summary>
    public IReadOnlyList<ClientDto> GetExpiredSubscriptions()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        return [.. repository.GetAll()
            .Where(client => client.SubscriptionEndDate < today)
            .OrderBy(client => client.FullName)
            .Select(ToDto)];
    }

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    public ClientDto Create(CreateClientDto dto)
    {
        Validate(dto);
        CheckPassport(dto.PassportNumber);

        var client = ToEntity(dto);
        return ToDto(repository.Add(client));
    }

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    public bool Update(int id, CreateClientDto dto)
    {
        if (repository.GetById(id) is null)
        {
            return false;
        }

        Validate(dto);
        CheckPassport(dto.PassportNumber, id);

        return repository.Update(id, ToEntity(dto));
    }

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    public bool Delete(int id) => repository.Delete(id);

    /// <summary>
    /// Проверить корректность данных клиента.
    /// </summary>
    private static void Validate(CreateClientDto dto)
    {
        if (dto.BirthDate == default || dto.BirthDate >= DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException(
                "Некорректная дата рождения.");
        }

        if (dto.SubscriptionStartDate == default ||
            dto.SubscriptionEndDate == default ||
            dto.SubscriptionStartDate > dto.SubscriptionEndDate)
        {
            throw new ArgumentException(
                "Некорректные даты абонемента.");
        }
    }

    /// <summary>
    /// Проверить уникальность номера паспорта.
    /// </summary>
    private void CheckPassport(string passportNumber, int? currentId = null)
    {
        if (repository.GetAll().Any(client =>
            client.PassportNumber == passportNumber &&
            client.Id != currentId))
        {
            throw new InvalidOperationException(
                "Клиент с таким номером паспорта уже существует.");
        }
    }

    /// <summary>
    /// Преобразовать DTO в сущность.
    /// </summary>
    private static Client ToEntity(CreateClientDto dto) => new()
    {
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        PhoneNumber = dto.PhoneNumber,
        SubscriptionStartDate = dto.SubscriptionStartDate,
        SubscriptionEndDate = dto.SubscriptionEndDate
    };

    /// <summary>
    /// Преобразовать сущность в DTO.
    /// </summary>
    private static ClientDto ToDto(Client client) => new(
        client.Id,
        client.PassportNumber,
        client.FullName,
        client.Gender,
        client.BirthDate,
        client.PhoneNumber,
        client.SubscriptionStartDate,
        client.SubscriptionEndDate);
}
