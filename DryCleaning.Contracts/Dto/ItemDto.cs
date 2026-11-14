namespace DryCleaning.Contracts.Dto;

public class ItemDto
{
    public int ItemId { get; set; }
    public required string Name { get; set; }
    public required string Material { get; set; }
    public required ItemCategoryDto Category { get; set; }
}
