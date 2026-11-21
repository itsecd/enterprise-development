using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;
/// <summary>
/// Преобразует данные залов
/// </summary>
public static class HallMapper
{
    /// <summary>
    /// Преобразует зал в DTO
    /// </summary>
    public static HallDto ToDto(this Hall hall) =>
        new(
            Id: hall.Id,
            Name: hall.Name);
    /// <summary>
    /// Создаёт зал из DTO
    /// </summary>
    public static Hall ToEntity(this HallEditDto hallEditDto) =>
        new() { Name = hallEditDto.Name };
    /// <summary>
    /// Обновляет зал данными из DTO
    /// </summary>
    public static void UpdateEntity(this Hall hall, HallEditDto hallEditDto)
    {
        hall.Name = hallEditDto.Name;
    }
}