namespace DryCleaning.Domain;

/// <summary>Клиент химчистки.</summary>
public class Client
{
    public int ClientId { get; set; }
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
}
