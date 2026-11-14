namespace DryCleaning.Contracts.Dto;

public class CategoryPopularityEntryDto
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public int ItemCount { get; set; }
}
