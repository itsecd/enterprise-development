using FitnessClub.Contracts.Dtos;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Выполняет преобразование зала в DTO.
/// </summary>
public static class HallMapping
{
    /// <summary>
    /// Преобразует сущность зала в DTO.
    /// </summary>
    /// <param name="hall">Зал фитнес-клуба.</param>
    /// <returns>DTO зала.</returns>
    public static HallDto ToDto(this Hall hall)
    {
        ArgumentNullException.ThrowIfNull(hall);

        return new HallDto
        {
            Id = hall.Id,
            Name = hall.Name,
            Capacity = hall.Capacity
        };
    }
}