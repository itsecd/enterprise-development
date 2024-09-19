namespace CityPharmacyChain.Domain;

public class Product(int productCode, int number, int count, string name, string productGroup)
{
    public int ProductCode { get; set; } = productCode;
    public int PharmacyNumber { get; set; } = number;
    public int Count { get; set; } = count;
    public string Name { get; set; } = name;
    public string ProductGroup { get; set; } = productGroup;
}
