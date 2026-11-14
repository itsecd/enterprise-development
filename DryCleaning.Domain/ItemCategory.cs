using DryCleaning.Domain.Enum;

namespace DryCleaning.Domain;

/// <summary>Категория изделия.</summary>
public class ItemCategory
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public required CleaningType RecommendedCleaningType { get; set; }
    public required decimal Price { get; set; }
}
