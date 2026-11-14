namespace DryCleaning.Contracts.Dto;

public class ClientDto
{
    public int ClientId { get; set; }
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
}
