using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Clients;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис для управления клиентами
/// </summary>
public class ClientService(
    IClientRepository repository,
    IOrderRepository orderRepository) : IClientService
{
    /// <summary>
    /// Получает всех клиентов
    /// </summary>
    /// <returns>Коллекция клиентов</returns>
    public IEnumerable<ClientDto> GetAll()
    {
        return repository.GetAll().Select(ToDto);
    }

    /// <summary>
    /// Получает клиента по идентификатору
    /// </summary>
    /// <returns>Данные клиента или null</returns>
    public ClientDto? GetById(int id)
    {
        var client = repository.GetById(id);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    /// <returns>Данные созданного клиента</returns>
    public ClientDto Create(CreateClientDto dto)
    {
        Validate(dto.FullName, dto.PhoneNumber, dto.DeliveryAddress);

        var client = new Client
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            DeliveryAddress = dto.DeliveryAddress
        };

        return ToDto(repository.Add(client));
    }

    /// <summary>
    /// Обновляет данные клиента
    /// </summary>
    /// <returns>true, если клиент обновлён, иначе false</returns>
    public bool Update(int id, UpdateClientDto dto)
    {
        var client = repository.GetById(id);
        if (client is null)
        {
            return false;
        }

        Validate(dto.FullName, dto.PhoneNumber, dto.DeliveryAddress);
        client.FullName = dto.FullName;
        client.PhoneNumber = dto.PhoneNumber;
        client.DeliveryAddress = dto.DeliveryAddress;
        repository.Update(client);

        return true;
    }

    /// <summary>
    /// Удаляет клиента без связанных заказов
    /// </summary>
    /// <returns>true, если клиент удалён, иначе false</returns>
    public bool Delete(int id)
    {
        var client = repository.GetById(id);
        if (client is null)
        {
            return false;
        }

        if (orderRepository.GetAll().Any(order => order.ClientId == id))
        {
            return false;
        }

        repository.Delete(client);
        return true;
    }

    private static ClientDto ToDto(Client client)
    {
        return new ClientDto(
            client.Id,
            client.FullName,
            client.PhoneNumber,
            client.DeliveryAddress);
    }

    private static void Validate(
        string fullName,
        string phoneNumber,
        string deliveryAddress)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("ФИО клиента не может быть пустым");
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Номер телефона не может быть пустым");
        }

        if (string.IsNullOrWhiteSpace(deliveryAddress))
        {
            throw new ArgumentException("Адрес доставки не может быть пустым");
        }
    }
}
