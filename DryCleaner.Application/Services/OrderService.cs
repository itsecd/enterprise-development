using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Services;

/// <summary>
/// Прикладные операции над заказами
/// </summary>
public class OrderService(
    IRepository<Order> orderRepository,
    IRepository<Customer> customerRepository,
    IRepository<Item> itemRepository) : IOrderService


{
    /// <inheritdoc />
    public IList<OrderDto> GetByCustomerId(int customerId)
    {
        return orderRepository.GetAll()
            .Where(o => o.CustomerId == customerId)
            .Select(order => new OrderDto(
                order.Id,
                new CustomerDto(order.Customer.Id, order.Customer.FirstName, order.Customer.Patronymic, order.Customer.LastName, order.Customer.PhoneNumber),
                order.CustomerId,
                new ItemDto(
                    order.Item.Id, order.Item.Name,
                    new ItemCategoryDto(order.Item.Category.Id, order.Item.Category.Name, order.Item.Category.RecommendedCleaning, order.Item.Category.Price),
                    order.Item.CategoryId, order.Item.Material),
                order.ItemId,
                order.AcceptanceDate,
                order.CompletionDays,
                order.Status))
            .ToList();
    }

    /// <inheritdoc />
    public IList<OrderDto> GetByItemId(int itemId)
    {
        return orderRepository.GetAll()
            .Where(o => o.ItemId == itemId)
            .Select(order => new OrderDto(
                order.Id,
                new CustomerDto(order.Customer.Id, order.Customer.FirstName, order.Customer.Patronymic, order.Customer.LastName, order.Customer.PhoneNumber),
                order.CustomerId,
                new ItemDto(
                    order.Item.Id, order.Item.Name,
                    new ItemCategoryDto(order.Item.Category.Id, order.Item.Category.Name, order.Item.Category.RecommendedCleaning, order.Item.Category.Price),
                    order.Item.CategoryId, order.Item.Material),
                order.ItemId,
                order.AcceptanceDate,
                order.CompletionDays,
                order.Status))
            .ToList();
    }

    /// <inheritdoc />
    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        var customer = customerRepository.GetById(dto.CustomerId) ?? throw new KeyNotFoundException($"Клиент с идентификатором {dto.CustomerId} не найден");

        var item = itemRepository.GetById(dto.ItemId) ?? throw new KeyNotFoundException($"Изделие с идентификатором {dto.ItemId} не найдено");

        var order = new Order
        {
            Id = 0,
            CustomerId = dto.CustomerId,
            Customer = customer,
            ItemId = dto.ItemId,
            Item = item,
            AcceptanceDate = dto.AcceptanceDate,
            CompletionDays = dto.CompletionDays,
            Status = dto.Status
        };

        var created = orderRepository.Add(order);

        return new OrderDto(
            created.Id,
            new CustomerDto(created.Customer.Id, created.Customer.FirstName, created.Customer.Patronymic, created.Customer.LastName, created.Customer.PhoneNumber),
            created.CustomerId,
            new ItemDto(
                created.Item.Id,
                created.Item.Name,
                new ItemCategoryDto(created.Item.Category.Id, created.Item.Category.Name, created.Item.Category.RecommendedCleaning, created.Item.Category.Price),
                created.Item.CategoryId,
                created.Item.Material),
            created.ItemId,
            created.AcceptanceDate,
            created.CompletionDays,
            created.Status);
    }

    /// <inheritdoc />
    public OrderDto? Get(int id)
    {
        var order = orderRepository.GetById(id);
        if (order is null) return null;

        return new OrderDto(
            order.Id,
            new CustomerDto(order.Customer.Id, order.Customer.FirstName, order.Customer.Patronymic, order.Customer.LastName, order.Customer.PhoneNumber),
            order.CustomerId,
            new ItemDto(
                order.Item.Id,
                order.Item.Name,
                new ItemCategoryDto(order.Item.Category.Id, order.Item.Category.Name, order.Item.Category.RecommendedCleaning, order.Item.Category.Price),
                order.Item.CategoryId,
                order.Item.Material),
            order.ItemId,
            order.AcceptanceDate,
            order.CompletionDays,
            order.Status);
    }

    /// <inheritdoc />
    public IList<OrderDto> GetAll()
    {
        return orderRepository.GetAll()
            .Select(order => new OrderDto(
                order.Id,
                new CustomerDto(order.Customer.Id, order.Customer.FirstName, order.Customer.Patronymic, order.Customer.LastName, order.Customer.PhoneNumber),
                order.CustomerId,
                new ItemDto(
                    order.Item.Id,
                    order.Item.Name,
                    new ItemCategoryDto(order.Item.Category.Id, order.Item.Category.Name, order.Item.Category.RecommendedCleaning, order.Item.Category.Price),
                    order.Item.CategoryId,
                    order.Item.Material),
                order.ItemId,
                order.AcceptanceDate,
                order.CompletionDays,
                order.Status))
            .ToList();
    }

    /// <inheritdoc />
    public OrderDto Update(OrderCreateUpdateDto dto, int id)
    {
        var customer = customerRepository.GetById(dto.CustomerId) ?? throw new KeyNotFoundException($"Клиент с идентификатором {dto.CustomerId} не найден");

        var item = itemRepository.GetById(dto.ItemId) ?? throw new KeyNotFoundException($"Изделие с идентификатором {dto.ItemId} не найдено");

        var order = new Order
        {
            Id = id,
            CustomerId = dto.CustomerId,
            Customer = customer,
            ItemId = dto.ItemId,
            Item = item,
            AcceptanceDate = dto.AcceptanceDate,
            CompletionDays = dto.CompletionDays,
            Status = dto.Status
        };

        var updated = orderRepository.Update(order);
        if (updated is null)
        {
            throw new KeyNotFoundException($"Заказ с идентификатором {id} не найден");
        }

        return new OrderDto(
            updated.Id,
            new CustomerDto(updated.Customer.Id, updated.Customer.FirstName, updated.Customer.Patronymic, updated.Customer.LastName, updated.Customer.PhoneNumber),
            updated.CustomerId,
            new ItemDto(
                updated.Item.Id,
                updated.Item.Name,
                new ItemCategoryDto(updated.Item.Category.Id, updated.Item.Category.Name, updated.Item.Category.RecommendedCleaning, updated.Item.Category.Price),
                updated.Item.CategoryId,
                updated.Item.Material),
            updated.ItemId,
            updated.AcceptanceDate,
            updated.CompletionDays,
            updated.Status);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        return orderRepository.Delete(id);
    }
}