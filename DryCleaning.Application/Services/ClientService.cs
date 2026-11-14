using System.ComponentModel.DataAnnotations;
using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Exceptions;
using DryCleaning.Contracts.Interfaces;
using DryCleaning.Domain;
using DryCleaning.Domain.Enum;
using DryCleaning.Domain.Interfaces;

namespace DryCleaning.Application.Services;

public class ClientService(
    IClientRepository repository,
    IOrderRepository orderRepository) : IClientService
{
    private readonly IClientRepository _repository = repository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();
        return [.. clients.Select(MapToDto)];
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        return client is null ? null : MapToDto(client);
    }

    public async Task<ClientDto> AddAsync(ClientEditDto client)
    {
        Validate(client);
        var newClient = new Client { FullName = client.FullName, PhoneNumber = client.PhoneNumber };
        var created = await _repository.AddAsync(newClient);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, ClientEditDto client)
    {
        Validate(client);
        var updated = new Client { ClientId = id, FullName = client.FullName, PhoneNumber = client.PhoneNumber };
        return await _repository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client is null) return false;
        if (await _orderRepository.HasOrdersForClientAsync(id))
            throw new ClientHasOrdersException(id);
        return await _repository.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<TopClientDto>> GetTopClientsAsync(DateTime start, DateTime end)
    {
        if (start >= end) throw new ArgumentException("Начало периода должно быть раньше конца.", nameof(start));

        var orders = await _orderRepository.GetAllAsync();
        return [.. orders
            .Where(order => order.AcceptDate >= start && order.AcceptDate < end && order.Status != OrderStatus.Cancelled)
            .GroupBy(order => order.Client.ClientId)
            .Select(group => new { Client = group.First().Client, ItemCount = group.Count() })
            .OrderByDescending(result => result.ItemCount)
            .ThenBy(result => result.Client.FullName, StringComparer.Ordinal)
            .ThenBy(result => result.Client.ClientId)
            .Take(5)
            .Select(result => new TopClientDto
            {
                ClientId = result.Client.ClientId,
                FullName = result.Client.FullName,
                PhoneNumber = result.Client.PhoneNumber,
                ItemCount = result.ItemCount
            })];
    }

    public async Task<TopSpenderDto?> GetTopSpenderAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var topSpender = orders
            .Where(order => order.Status == OrderStatus.Issued)
            .GroupBy(order => order.Client.ClientId)
            .Select(group => new { Client = group.First().Client, TotalSpent = group.Sum(o => o.Item.Category.Price) })
            .OrderByDescending(result => result.TotalSpent)
            .ThenBy(result => result.Client.FullName, StringComparer.Ordinal)
            .ThenBy(result => result.Client.ClientId)
            .FirstOrDefault();

        return topSpender is null ? null : new TopSpenderDto { Client = MapToDto(topSpender.Client), TotalSpent = topSpender.TotalSpent };
    }

    public async Task<LongestProcessingDto?> GetLongestProcessingOrderAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var completedOrders = orders
            .Where(order => (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Issued) && order.CompletedDate.HasValue)
            .Select(order => new { order.Client, Duration = order.CompletedDate!.Value - order.AcceptDate })
            .ToList();

        if (completedOrders.Count == 0) return null;

        var maxDuration = completedOrders.Max(order => order.Duration);
        var clients = completedOrders
            .Where(order => order.Duration == maxDuration)
            .Select(order => order.Client)
            .DistinctBy(client => client.ClientId)
            .OrderBy(client => client.FullName, StringComparer.Ordinal)
            .ThenBy(client => client.ClientId)
            .Select(MapToDto)
            .ToList();

        return new LongestProcessingDto { DurationDays = maxDuration.TotalDays, Clients = clients };
    }

    private static ClientDto MapToDto(Client client) => new() { ClientId = client.ClientId, FullName = client.FullName, PhoneNumber = client.PhoneNumber };

    private static void Validate(object request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);
    }
}
