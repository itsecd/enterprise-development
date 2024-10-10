namespace EnterpriseWarehouse.API.DTO;

public class ProductSupplyToOrganizationsDTO
{
    public int TotalQuantity { get; set; }
    public required string ProductName { get; set; }
    public required string OrganizationName { get; set; }
}
