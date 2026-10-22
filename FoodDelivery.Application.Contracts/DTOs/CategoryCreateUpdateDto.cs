using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления категории блюд
/// </summary>
/// <param name="Name">Название категории</param>
public record CategoryCreateUpdateDto(
    [property: Required(ErrorMessage = "Название категории обязательно")]
    [property: StringLength(100, MinimumLength = 1, ErrorMessage = "Название категории не должно превышать 100 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Название категории не должно состоять только из пробелов")]
    string Name
);