using System.ComponentModel.DataAnnotations;
using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using DryCleaning.Domain;
using DryCleaning.Domain.Enum;
using DryCleaning.Domain.Interfaces;

namespace DryCleaning.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IClientRepository clientRepository,
    IItemRepository itemRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task<IReadOnlyList<OrderDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return [.. orders.Select(MapToDto)];
    }

    public async Task<IReadOnlyList<OrderDto>?> GetByClientIdAsync(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client is null) return null;
        var orders = await _orderRepository.GetByClientIdAsync(clientId);
        return [.. orders.Select(MapToDto)];
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order is null ? null : MapToDto(order);
    }

    public async Task<OrderDto> AddAsync(OrderCreateDto order)
    {
        Validate(order);
        var client = await _clientRepository.GetByIdAsync(order.ClientId) ?? throw new ValidationException("Клиент не найден");
        var item = await _itemRepository.GetByIdAsync(order.ItemId) ?? throw new ValidationException("Изделие не найдено");

        var newOrder = new Order
        {
            Client = client,
            Item = item,
            ProcessingDays = order.ProcessingDays,
            Status = OrderStatus.Accepted,
            AcceptDate = DateTime.UtcNow,
            CompletedDate = null
        };
        var created = await _orderRepository.AddAsync(newOrder);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, OrderEditDto order)
    {
        Validate(order);
        var existing = await _orderRepository.GetByIdAsync(id);
        if (existing is null) return false;

        var completedDate = ValidateCompletedDate(order, existing.AcceptDate);
        var client = await _clientRepository.GetByIdAsync(order.ClientId) ?? throw new ValidationException("Клиент не найден");
        var item = await _itemRepository.GetByIdAsync(order.ItemId) ?? throw new ValidationException("Изделие не найдено");

        var updated = new Order
        {
            Id = id,
            Client = client,
            Item = item,
            Status = order.Status,
            ProcessingDays = order.ProcessingDays,
            AcceptDate = existing.AcceptDate,
            CompletedDate = completedDate
        };
        return await _orderRepository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await _orderRepository.DeleteAsync(id);

    public async Task<IReadOnlyList<OrderDto>> GetInProgressAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return [.. orders
            .Where(order => order.Status == OrderStatus.InProgress)
            .OrderBy(order => order.AcceptDate)
            .ThenBy(order => order.Id)
            .Select(MapToDto)];
    }

    private static OrderDto MapToDto(Order order) => new()
    {
        Id = order.Id,
        Client = new ClientDto { ClientId = order.Client.ClientId, FullName = order.Client.FullName, PhoneNumber = order.Client.PhoneNumber },
        Item = new ItemDto { ItemId = order.Item.ItemId, Name = order.Item.Name, Material = order.Item.Material, Category = new ItemCategoryDto { CategoryId = order.Item.Category.CategoryId, CategoryName = order.Item.Category.CategoryName, RecommendedCleaningType = order.Item.Category.RecommendedCleaningType, Price = order.Item.Category.Price } },
        Status = order.Status,
        ProcessingDays = order.ProcessingDays,
        AcceptDate = order.AcceptDate,
        CompletedDate = order.CompletedDate
    };

    private static void Validate(object request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);
    }

    private static DateTime? ValidateCompletedDate(OrderEditDto order, DateTime acceptDate)
    {
        var finished = order.Status is OrderStatus.Completed or OrderStatus.Issued;
        if (finished && order.CompletedDate is null) throw new ValidationException("Для завершенного заказа нужна дата завершения");
        if (!finished && order.CompletedDate is not null) throw new ValidationException("У незавершенного заказа не должно быть даты завершения");
        if (order.CompletedDate is not DateTime completed) return null;
        if (completed < acceptDate) throw new ValidationException("Дата завершения не может быть раньше даты приема");
        return completed;
    }
}
