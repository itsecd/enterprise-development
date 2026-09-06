using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

public class Driver
{ 
    public int Id { get; set; }
    public string PassportNumber { get; set; }
    public string FullName { get; set; }
    public int Experience { get; set; }
    public DrivingLicence Licence { get; set; }
}