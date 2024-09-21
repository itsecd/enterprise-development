namespace CityPharmacyChain.Domain;

public class PriceListEntry(int productCode, int pharmacyNumber, int soldCount, string manufacturer, PaymentType paymentType, DateTime saleDate)
{
    public int ProductCode { get; set; } = productCode;
    public int PharmacyNumber { get; set; } = pharmacyNumber;
    public int SoldCount { get; set; } = soldCount;
    public string Manufacturer { get; set; } = manufacturer;
    public PaymentType PaymentType { get; set; } = paymentType;
    public DateTime SaleDate { get; set; } = saleDate;
}
