namespace CityPharmacyChain.Domain;

public class PharmacyProduct(int productCode, int pharmacyNumber, int count, double price)
{
    public int ProductCode { get; set; } = productCode;
    public int PharmacyNumber { get; set; } = pharmacyNumber;
    public int Count { get; set; } = count;
    public double Price { get; set; } = price;
}
