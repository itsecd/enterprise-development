namespace CityPharmacyChain.Domain;

public class PharmaceuticalGroup(int productCode, string name)
{
    public int ProductCode { get; set; } = productCode;
    public string Name { get; set; } = name;
}