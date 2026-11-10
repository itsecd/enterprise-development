using System.ComponentModel.DataAnnotations;
using VetClinic.Domain.Enums;

namespace VetClinic.Application.Dto;

/// <summary>
/// Данные для создания или обновления породы
/// </summary>
public class CreateBreedDto
{
    /// <summary>
    /// Название породы
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    [Required]
    public AnimalSpecies? Species { get; set; }
}