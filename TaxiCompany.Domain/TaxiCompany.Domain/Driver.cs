namespace TaxiCompany.Domain;

public class Driver
{
    public required int Id { get; set; }

    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Passport { get; set; }

    public required string Address { get; set; }

    public required int AssignedCard { get; set; }
}
