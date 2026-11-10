using Museum.Application.Mapping;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Application.Services;

/// <summary>Управляет билетами и проверяет связанные с ними экскурсии и посетителей</summary>
public sealed class TicketService(
    ITicketRepository ticketRepository,
    ITourRepository tourRepository,
    IVisitorRepository visitorRepository) : ITicketService
{
    /// <summary>Возвращает список всех билетов</summary>
    public async Task<IReadOnlyList<TicketDto>> GetAllAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();
        return tickets.Select(TicketMapper.ToDto).ToList();
    }

    /// <summary>Находит билет по его идентификатору</summary>
    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        return ticket is null ? null : TicketMapper.ToDto(ticket);
    }

    /// <summary>Оформляет билет для выбранной экскурсии и посетителя</summary>
    public async Task<TicketDto> CreateAsync(TicketEditDto dto)
    {
        var (tour, visitor) = await GetRelatedEntitiesAsync(dto);
        var ticket = TicketMapper.ToEntity(dto, tour, visitor);
        var createdTicket = await ticketRepository.AddAsync(ticket);

        return TicketMapper.ToDto(createdTicket);
    }

    /// <summary>Изменяет сведения о билете</summary>
    public async Task<bool> UpdateAsync(int id, TicketEditDto dto)
    {
        var existingTicket = await ticketRepository.GetByIdAsync(id);

        if (existingTicket is null)
            return false;

        var (tour, visitor) = await GetRelatedEntitiesAsync(dto);
        var updatedTicket = TicketMapper.ToEntity(dto, tour, visitor);
        updatedTicket.Id = id;

        return await ticketRepository.UpdateAsync(updatedTicket);
    }

    /// <summary>Удаляет билет по его идентификатору</summary>
    public Task<bool> DeleteAsync(int id)
    {
        return ticketRepository.DeleteAsync(id);
    }

    /// <summary>Находит экскурсию и посетителя, указанных в данных билета</summary>
    private async Task<(Tour Tour, Visitor Visitor)> GetRelatedEntitiesAsync(
        TicketEditDto dto)
    {
        var tour = await tourRepository.GetByIdAsync(dto.TourId);
        var visitor = await visitorRepository.GetByIdAsync(dto.VisitorId);

        if (tour is null || visitor is null)
            throw new ArgumentException("Экскурсия или посетитель не найдены");

        return (tour, visitor);
    }
}
