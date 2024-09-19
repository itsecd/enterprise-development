namespace CityPharmacyChain.Domain;

public class Product(long code, int count, string name, string productGroup)
{
    public long Code { get; set; } = code;
    public int Count { get; set; } = count;
    public string Name { get; set; } = name;
    public string ProductGroup { get; set; } = productGroup;
    public List<string> PharmaceuticalGroup { get; set; } = [];
}
