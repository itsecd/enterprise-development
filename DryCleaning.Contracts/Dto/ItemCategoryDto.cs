using DryCleaning.Domain.Enum;

namespace DryCleaning.Contracts.Dto;

public class ItemCategoryDto
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public required CleaningType RecommendedCleaningType { get; set; }
    public required decimal Price { get; set; }
}
