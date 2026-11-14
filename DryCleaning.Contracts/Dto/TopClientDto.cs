namespace DryCleaning.Contracts.Dto;

public class TopClientDto
{
    public int ClientId { get; set; }
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
    public int ItemCount { get; set; }
}
