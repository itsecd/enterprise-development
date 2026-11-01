using AutoServiceLab.Application.Contracts.DTOs.Clients;
using AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public class ClientService(
    IClientRepository repository,
    IServiceOrderRepository orderRepository,
    ILogger<ClientService> logger) : IClientService
{
    /// <inheritdoc />
    public IList<ClientDto> GetAll()
    {
        logger.LogInformation("Получение списка всех клиентов");
        return repository.GetAll()
            .Select(c => new ClientDto(c.Id, c.FullName, c.Phone))
            .ToList();
    }

    /// <inheritdoc />
    public ClientDto? Get(int id)
    {
        logger.LogInformation("Получение клиента с ID: {Id}", id);
        var client = repository.Get(id);
        if (client == null)
        {
            logger.LogWarning("Клиент с ID: {Id} не найден", id);
            return null;
        }
        return new ClientDto(client.Id, client.FullName, client.Phone);
    }

    /// <inheritdoc />
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        logger.LogInformation("Создание нового клиента: {FullName}", dto.FullName);
        var client = new Client { FullName = dto.FullName, Phone = dto.Phone };
        repository.Add(client);
        logger.LogInformation("Клиент успешно создан с ID: {Id}", client.Id);
        return new ClientDto(client.Id, client.FullName, client.Phone);
    }

    /// <inheritdoc />
    public ClientDto Update(ClientCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление клиента с ID: {Id}", id);
        var client = repository.Get(id) ?? throw new KeyNotFoundException($"Клиент {id} не найден");
        client.FullName = dto.FullName;
        client.Phone = dto.Phone;
        repository.Update(client);
        return new ClientDto(client.Id, client.FullName, client.Phone);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление клиента с ID: {Id}", id);
        return repository.Delete(id);
    }

    /// <inheritdoc />
    public IList<ServiceOrderDto> GetOrdersByClient(int clientId)
    {
        logger.LogInformation("Получение заказов клиента с ID: {ClientId}", clientId);
        return orderRepository.GetAll()
            .Where(o => o.ClientId == clientId)
            .Select(o => new ServiceOrderDto(o.Id, o.ReceptionDate, o.IssueDate, o.ClientId, o.Client?.FullName ?? "", o.CarId, o.Car?.Model ?? ""))
            .ToList();
    }
}
