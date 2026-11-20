using AutoService.Contracts.Dtos;

namespace AutoService.Application.Interfaces;

/// <summary>Операции с механиками.</summary>
public interface IMechanicService : ICrudService<MechanicRequest, MechanicDto>
{
    /// <summary>Возвращает заказы механика.</summary>
    public Task<IReadOnlyList<OrderDto>> GetOrdersAsync(int id);
    /// <summary>Клиенты механика в порядке ФИО.</summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsAsync(int id);
}
