using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания или обновления породы
/// </summary>
public class CreateBreedDto
{
    /// <summary>
    /// Название породы
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }
}