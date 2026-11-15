using Museum.Contracts.Dto;
using Museum.Domain;

namespace Museum.Application.Mapping;

/// <summary>
/// Преобразует данные экскурсий в DTO и обратно
/// </summary>
public static class TourMapper
{
    /// <summary>
    /// Преобразует экскурсию в DTO
    /// </summary>
    public static TourDto ToDto(Tour tour)
    {
        return new TourDto
        {
            Id = tour.Id,
            Date = tour.Date,
            StartTime = tour.StartTime,
            DurationMinutes = tour.DurationMinutes,
            Exhibitions = tour.Exhibitions
                .Select(ExhibitionMapper.ToDto)
                .ToList()
        };
    }

    /// <summary>
    /// Создаёт экскурсию с выбранными выставками
    /// </summary>
    public static Tour ToEntity(
        TourEditDto dto,
        IReadOnlyList<Exhibition> exhibitions)
    {
        return new Tour
        {
            Date = dto.Date,
            StartTime = dto.StartTime,
            DurationMinutes = dto.DurationMinutes,
            Exhibitions = exhibitions.ToList()
        };
    }
}
