namespace DryCleaning.Contracts.Dto;

public class LongestProcessingDto
{
    public double DurationDays { get; set; }
    public required IReadOnlyList<ClientDto> Clients { get; set; }
}
