using Museum.Application.Mapping;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain.Interfaces;

namespace Museum.Application.Services;

/// <summary>
/// Управляет посетителями музея
/// </summary>
public sealed class VisitorService(
    IVisitorRepository visitorRepository,
    ITicketRepository ticketRepository) : IVisitorService
{
    /// <summary>
    /// Возвращает всех посетителей музея
    /// </summary>
    public async Task<IReadOnlyList<VisitorDto>> GetAllAsync()
    {
        var visitors = await visitorRepository.GetAllAsync();

        return visitors.Select(VisitorMapper.ToDto).ToList();
    }

    /// <summary>
    /// Находит посетителя по идентификатору
    /// </summary>
    public async Task<VisitorDto?> GetByIdAsync(int id)
    {
        var visitor = await visitorRepository.GetByIdAsync(id);

        return visitor is null ? null : VisitorMapper.ToDto(visitor);
    }

    /// <summary>
    /// Добавляет нового посетителя
    /// </summary>
    public async Task<VisitorDto> CreateAsync(VisitorEditDto dto)
    {
        var visitor = VisitorMapper.ToEntity(dto);
        var created = await visitorRepository.AddAsync(visitor);

        return VisitorMapper.ToDto(created);
    }

    /// <summary>
    /// Обновляет информацию о посетителе
    /// </summary>
    public async Task<bool> UpdateAsync(int id, VisitorEditDto dto)
    {
        var existing = await visitorRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return false;
        }

        var visitor = VisitorMapper.ToEntity(dto);
        visitor.Id = id;

        return await visitorRepository.UpdateAsync(visitor);
    }

    /// <summary>
    /// Удаляет посетителя, если у него нет билетов
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var visitor = await visitorRepository.GetByIdAsync(id);

        if (visitor is null)
        {
            return false;
        }

        var tickets = await ticketRepository.GetAllAsync();

        if (tickets.Any(ticket => ticket.Visitor.Id == id))
        {
            throw new InvalidOperationException(
                "У посетителя есть билеты");
        }

        return await visitorRepository.DeleteAsync(id);
    }
}
