using System.ComponentModel.DataAnnotations;

namespace DryCleaner.Application.Contracts.DTOs.Item;

/// <summary>
/// DTO для создания или обновления изделия
/// </summary>
/// <param name="Name">Название изделия</param>
/// <param name="CategoryId">Идентификатор категории</param>
/// <param name="Material">Материал изделия</param>
public record ItemCreateUpdateDto(
    [Required(ErrorMessage = "Название изделия обязательно")]
    string Name,

    int CategoryId,

    [Required(ErrorMessage = "Материал изделия обязателен для заполнения!")]
    string Material
);