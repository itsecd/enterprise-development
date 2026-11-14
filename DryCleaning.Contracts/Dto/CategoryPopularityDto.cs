namespace DryCleaning.Contracts.Dto;

public class CategoryPopularityDto
{
    public required IReadOnlyList<CategoryPopularityEntryDto> MostPopular { get; set; }
    public required IReadOnlyList<CategoryPopularityEntryDto> LeastPopular { get; set; }
}
