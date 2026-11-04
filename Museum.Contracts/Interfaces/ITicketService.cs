using Museum.Contracts.Dto;

namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает работу с билетами музея
/// </summary>
public interface ITicketService
    : IEntityService<TicketDto, TicketEditDto>;
