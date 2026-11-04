using Museum.Application.Mapping;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain.Interfaces;

namespace Museum.Application.Services;

/// <summary>
/// Управляет выставками музея
/// </summary>
public sealed class ExhibitionService : IExhibitionService
{
    private readonly IExhibitionRepository _exhibitionRepository;
    private readonly ITourRepository _tourRepository;

    /// <summary>
    /// Создаёт сервис для работы с выставками
    /// </summary>
    public ExhibitionService(
        IExhibitionRepository exhibitionRepository,
        ITourRepository tourRepository)
    {
        _exhibitionRepository = exhibitionRepository;
        _tourRepository = tourRepository;
    }

    /// <summary>
    /// Возвращает все выставки музея
    /// </summary>
    public async Task<IReadOnlyList<ExhibitionDto>> GetAllAsync()
    {
        var exhibitions = await _exhibitionRepository.GetAllAsync();

        return exhibitions.Select(ExhibitionMapper.ToDto).ToList();
    }

    /// <summary>
    /// Находит выставку по идентификатору
    /// </summary>
    public async Task<ExhibitionDto?> GetByIdAsync(int id)
    {
        var exhibition = await _exhibitionRepository.GetByIdAsync(id);

        return exhibition is null ? null : ExhibitionMapper.ToDto(exhibition);
    }

    /// <summary>
    /// Добавляет новую выставку
    /// </summary>
    public async Task<ExhibitionDto> CreateAsync(ExhibitionEditDto dto)
    {
        var exhibition = ExhibitionMapper.ToEntity(dto);
        var created = await _exhibitionRepository.AddAsync(exhibition);

        return ExhibitionMapper.ToDto(created);
    }

    /// <summary>
    /// Обновляет информацию о выставке
    /// </summary>
    public async Task<bool> UpdateAsync(int id, ExhibitionEditDto dto)
    {
        var existing = await _exhibitionRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return false;
        }

        var exhibition = ExhibitionMapper.ToEntity(dto);
        exhibition.Id = id;

        return await _exhibitionRepository.UpdateAsync(exhibition);
    }

    /// <summary>
    /// Удаляет выставку, если она не используется в экскурсиях
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var exhibition = await _exhibitionRepository.GetByIdAsync(id);

        if (exhibition is null)
        {
            return false;
        }

        var tours = await _tourRepository.GetAllAsync();

        if (tours.Any(tour =>
                tour.Exhibitions.Any(item => item.Id == id)))
        {
            throw new InvalidOperationException(
                "Выставка используется в экскурсиях");
        }

        return await _exhibitionRepository.DeleteAsync(id);
    }
}
