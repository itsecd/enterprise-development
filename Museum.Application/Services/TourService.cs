using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain;
using Museum.Domain.Interfaces;
using Museum.Application.Mapping;

namespace Museum.Application.Services;

/// <summary>Управляет экскурсиями и проверяет связанные с ними данные</summary>
public sealed class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
    private readonly IExhibitionRepository _exhibitionRepository;
    private readonly ITicketRepository _ticketRepository;

    /// <summary>Создаёт сервис экскурсий с необходимыми репозиториями</summary>
    public TourService(
        ITourRepository tourRepository,
        IExhibitionRepository exhibitionRepository,
        ITicketRepository ticketRepository)
    {
        _tourRepository = tourRepository;
        _exhibitionRepository = exhibitionRepository;
        _ticketRepository = ticketRepository;
    }

    /// <summary>Возвращает список всех экскурсий</summary>
    public async Task<IReadOnlyList<TourDto>> GetAllAsync()
    {
        var tours = await _tourRepository.GetAllAsync();
        return tours.Select(TourMapper.ToDto).ToList();
    }

    /// <summary>Находит экскурсию по её идентификатору</summary>
    public async Task<TourDto?> GetByIdAsync(int id)
    {
        var tour = await _tourRepository.GetByIdAsync(id);
        return tour is null ? null : TourMapper.ToDto(tour);
    }

    /// <summary>Создаёт экскурсию с выбранными выставками</summary>
    public async Task<TourDto> CreateAsync(TourEditDto dto)
    {
        var exhibitions = await GetExhibitionsAsync(dto.ExhibitionIds);
        var tour = TourMapper.ToEntity(dto, exhibitions);
        var createdTour = await _tourRepository.AddAsync(tour);

        return TourMapper.ToDto(createdTour);
    }

    /// <summary>Обновляет сведения об экскурсии</summary>
    public async Task<bool> UpdateAsync(int id, TourEditDto dto)
    {
        var existingTour = await _tourRepository.GetByIdAsync(id);

        if (existingTour is null)
            return false;

        var exhibitions = await GetExhibitionsAsync(dto.ExhibitionIds);
        var updatedTour = TourMapper.ToEntity(dto, exhibitions);
        updatedTour.Id = id;

        return await _tourRepository.UpdateAsync(updatedTour);
    }

    /// <summary>Удаляет экскурсию, если на неё ещё не оформлены билеты</summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var tour = await _tourRepository.GetByIdAsync(id);

        if (tour is null)
            return false;

        var tickets = await _ticketRepository.GetAllAsync();

        if (tickets.Any(ticket => ticket.Tour.Id == id))
            throw new InvalidOperationException("На экскурсию есть билеты");

        return await _tourRepository.DeleteAsync(id);
    }

    /// <summary>Находит выставки по идентификаторам и проверяет их наличие</summary>
    private async Task<IReadOnlyList<Exhibition>> GetExhibitionsAsync(
        IReadOnlyList<int> exhibitionIds)
    {
        var allExhibitions = await _exhibitionRepository.GetAllAsync();
        var selectedExhibitions = new List<Exhibition>();

        foreach (var exhibitionId in exhibitionIds.Distinct())
        {
            var exhibition = allExhibitions.FirstOrDefault(
                item => item.Id == exhibitionId);

            if (exhibition is null)
                throw new ArgumentException("Одна или несколько выставок не найдены");

            selectedExhibitions.Add(exhibition);
        }

        return selectedExhibitions;
    }
}
