using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain.Interfaces;
using Museum.Application.Mapping;

namespace Museum.Application.Services;

/// <summary>Управляет билетами и проверяет связанные с ними экскурсии и посетителей</summary>
public sealed class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITourRepository _tourRepository;
    private readonly IVisitorRepository _visitorRepository;

    /// <summary>Создаёт сервис билетов с необходимыми репозиториями</summary>
    public TicketService(
        ITicketRepository ticketRepository,
        ITourRepository tourRepository,
        IVisitorRepository visitorRepository)
    {
        _ticketRepository = ticketRepository;
        _tourRepository = tourRepository;
        _visitorRepository = visitorRepository;
    }

    /// <summary>Возвращает список всех билетов</summary>
    public async Task<IReadOnlyList<TicketDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return tickets.Select(TicketMapper.ToDto).ToList();
    }

    /// <summary>Находит билет по его идентификатору</summary>
    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket is null ? null : TicketMapper.ToDto(ticket);
    }

    /// <summary>Оформляет билет для выбранной экскурсии и посетителя</summary>
    public async Task<TicketDto> CreateAsync(TicketEditDto dto)
    {
        var (tour, visitor) = await GetRelatedEntitiesAsync(dto);
        var ticket = TicketMapper.ToEntity(dto, tour, visitor);
        var createdTicket = await _ticketRepository.AddAsync(ticket);

        return TicketMapper.ToDto(createdTicket);
    }

    /// <summary>Изменяет сведения о билете</summary>
    public async Task<bool> UpdateAsync(int id, TicketEditDto dto)
    {
        var existingTicket = await _ticketRepository.GetByIdAsync(id);

        if (existingTicket is null)
            return false;

        var (tour, visitor) = await GetRelatedEntitiesAsync(dto);
        var updatedTicket = TicketMapper.ToEntity(dto, tour, visitor);
        updatedTicket.Id = id;

        return await _ticketRepository.UpdateAsync(updatedTicket);
    }

    /// <summary>Удаляет билет по его идентификатору</summary>
    public Task<bool> DeleteAsync(int id)
    {
        return _ticketRepository.DeleteAsync(id);
    }

    /// <summary>Находит экскурсию и посетителя, указанных в данных билета</summary>
    private async Task<(Museum.Domain.Tour Tour, Museum.Domain.Visitor Visitor)>
        GetRelatedEntitiesAsync(TicketEditDto dto)
    {
        var tour = await _tourRepository.GetByIdAsync(dto.TourId);
        var visitor = await _visitorRepository.GetByIdAsync(dto.VisitorId);

        if (tour is null || visitor is null)
            throw new ArgumentException("Экскурсия или посетитель не найдены");

        return (tour, visitor);
    }
}
