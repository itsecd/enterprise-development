using Museum.Contracts.Dto;
using Museum.Domain;

namespace Museum.Application.Mapping;

/// <summary>
/// Преобразует данные выставок в DTO и обратно
/// </summary>
public static class ExhibitionMapper
{
    /// <summary>
    /// Преобразует выставку в DTO
    /// </summary>
    public static ExhibitionDto ToDto(Exhibition exhibition)
    {
        return new ExhibitionDto
        {
            Id = exhibition.Id,
            Name = exhibition.Name,
            Theme = exhibition.Theme,
            HallNumber = exhibition.HallNumber,
            StartDate = exhibition.StartDate,
            EndDate = exhibition.EndDate
        };
    }

    /// <summary>
    /// Создаёт выставку из полученных данных
    /// </summary>
    public static Exhibition ToEntity(ExhibitionEditDto dto)
    {
        return new Exhibition
        {
            Name = dto.Name,
            Theme = dto.Theme,
            HallNumber = dto.HallNumber,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };
    }
}
