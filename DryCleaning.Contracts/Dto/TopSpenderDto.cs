namespace DryCleaning.Contracts.Dto;

public class TopSpenderDto
{
    public required ClientDto Client { get; set; }
    public decimal TotalSpent { get; set; }
}
