using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления блюда
/// </summary>
/// <param name="Name">Название блюда</param>
/// <param name="WeightGrams">Вес блюда в граммах</param>
/// <param name="Price">Цена блюда</param>
/// <param name="CategoryId">Идентификатор категории блюда</param>
/// <param name="RestaurantId">Идентификатор ресторана, которому принадлежит блюдо</param>
public record DishCreateUpdateDto(
    [property: Required(ErrorMessage = "Название блюда обязательно")]
    [property: StringLength(100, MinimumLength = 1, ErrorMessage = "Название блюда должно содержать не более 100 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Название блюда не должно состоять только из пробелов")]
    string Name,

    [property: Range(1, int.MaxValue, ErrorMessage = "Вес блюда должен быть больше нуля")]
    int WeightGrams,

    [property: Range(typeof(decimal), "0.01", "10000", ErrorMessage = "Цена блюда должна быть больше нуля")]
    decimal Price,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор категории должен быть положительным")]
    int CategoryId,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор ресторана должен быть положительным")]
    int RestaurantId
);