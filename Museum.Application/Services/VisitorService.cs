using Museum.Application.Mapping;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain.Interfaces;

namespace Museum.Application.Services;

/// <summary>
/// Управляет посетителями музея
/// </summary>
public sealed class VisitorService : IVisitorService
{
    private readonly IVisitorRepository _visitorRepository;
    private readonly ITicketRepository _ticketRepository;

    /// <summary>
    /// Создаёт сервис для работы с посетителями
    /// </summary>
    public VisitorService(
        IVisitorRepository visitorRepository,
        ITicketRepository ticketRepository)
    {
        _visitorRepository = visitorRepository;
        _ticketRepository = ticketRepository;
    }

    /// <summary>
    /// Возвращает всех посетителей музея
    /// </summary>
    public async Task<IReadOnlyList<VisitorDto>> GetAllAsync()
    {
        var visitors = await _visitorRepository.GetAllAsync();

        return visitors.Select(VisitorMapper.ToDto).ToList();
    }

    /// <summary>
    /// Находит посетителя по идентификатору
    /// </summary>
    public async Task<VisitorDto?> GetByIdAsync(int id)
    {
        var visitor = await _visitorRepository.GetByIdAsync(id);

        return visitor is null ? null : VisitorMapper.ToDto(visitor);
    }

    /// <summary>
    /// Добавляет нового посетителя
    /// </summary>
    public async Task<VisitorDto> CreateAsync(VisitorEditDto dto)
    {
        var visitor = VisitorMapper.ToEntity(dto);
        var created = await _visitorRepository.AddAsync(visitor);

        return VisitorMapper.ToDto(created);
    }

    /// <summary>
    /// Обновляет информацию о посетителе
    /// </summary>
    public async Task<bool> UpdateAsync(int id, VisitorEditDto dto)
    {
        var existing = await _visitorRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return false;
        }

        var visitor = VisitorMapper.ToEntity(dto);
        visitor.Id = id;

        return await _visitorRepository.UpdateAsync(visitor);
    }

    /// <summary>
    /// Удаляет посетителя, если у него нет билетов
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var visitor = await _visitorRepository.GetByIdAsync(id);

        if (visitor is null)
        {
            return false;
        }

        var tickets = await _ticketRepository.GetAllAsync();

        if (tickets.Any(ticket => ticket.Visitor.Id == id))
        {
            throw new InvalidOperationException(
                "У посетителя есть билеты");
        }

        return await _visitorRepository.DeleteAsync(id);
    }
}
