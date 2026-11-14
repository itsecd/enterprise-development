using System.ComponentModel.DataAnnotations;
using DryCleaning.Domain.Enum;

namespace DryCleaning.Contracts.Dto;

public class ItemCategoryEditDto
{
    [Required, MaxLength(50)]
    public required string CategoryName { get; set; }

    [Required, EnumDataType(typeof(CleaningType))]
    public required CleaningType RecommendedCleaningType { get; set; }

    [Required]
    [Range(typeof(decimal), "1", "10000000", ErrorMessage = "Цена должна быть от 1 до 10 000 000.")]
    public required decimal Price { get; set; }
}
