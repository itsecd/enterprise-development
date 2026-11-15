using Museum.Contracts.Dto;
using Museum.Domain;

namespace Museum.Application.Mapping;

/// <summary>
/// Преобразует данные посетителей в DTO и обратно
/// </summary>
public static class VisitorMapper
{
    /// <summary>
    /// Преобразует посетителя в DTO
    /// </summary>
    public static VisitorDto ToDto(Visitor visitor)
    {
        return new VisitorDto
        {
            Id = visitor.Id,
            FullName = visitor.FullName,
            Phone = visitor.Phone,
            BirthDate = visitor.BirthDate
        };
    }

    /// <summary>
    /// Создаёт посетителя из полученных данных
    /// </summary>
    public static Visitor ToEntity(VisitorEditDto dto)
    {
        return new Visitor
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            BirthDate = dto.BirthDate
        };
    }
}
