using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entitites;

public class Client
{ 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}